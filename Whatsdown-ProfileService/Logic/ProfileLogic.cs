using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Logic;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Views;
using Whatsdown_ProfileService.caching;
using Whatsdown_ProfileService.Data;
using Whatsdown_ProfileService.Views;

namespace Whatsdown_ProfileService.Logic
{
    public class ProfileLogic
    {
        private ProfileRepository repository;
        private readonly IMemoryCache mCache;
        private readonly ILogger<ProfileLogic> _logger;
        public ProfileLogic(ProfileContext context, IMemoryCache memoryCache, ILogger<ProfileLogic> logger)
        {
            _logger = logger;
            repository = new ProfileRepository(context);
            this.mCache = memoryCache;
        }


        public void PostProfile(PostProfileView view)
        {
            IsValid(view);

            Profile profile = new Profile(view.profileId, view.displayName, Variables.DefaultStatus, "", "Male");
            repository.saveProfile(profile);

        }

        public Profile GetProfileById(string id)
        {
            Profile profile = repository.GetProfileByProfileId(id);
            if (profile != null)
            return profile;
            throw new ArgumentException("Profile does not exist");
        }

        public List<Profile> GetProfiles(List<String> ids)
        {
            return repository.GetProfiles(ids);
        }

        private bool IsValid(PostProfileView view)
        {
            if (view.displayName == null || view.displayName == "")
                throw new ArgumentException("Nickname is mandatory.");
            if (view.profileId == null || view.profileId == "")
                throw new ArgumentException("ProfileId is mandatory.");
            if (view.gender == null || view.gender == "")
                throw new ArgumentException("Gender is mandatory.");

            if (view.displayName.Length < Variables.DefaultUsernameLength)
                throw new ArgumentException("Nickname needs to be at least 5 letters");

            return true;
        }

        public List<PotentialContactView> GetProfilesByName(string name, string profileId)
        {
            _logger.LogInformation($"GetProfilesByName() method called with parameters: name = {name} and profileId = {profileId}");
            
            if (name == null)
            {
                _logger.LogWarning($"GetProfilesByName() method failed because parameter name is null");
                throw new ArgumentException("Name has to be at least 5 characters long");
            }
            
            if (name.Length < 5)
            {
                _logger.LogWarning($"GetProfilesByName() method failed because parameter name length = {name.Length} while it should be 5");
                throw new ArgumentException("Name has to be at least 5 characters long");
            }
                   

            if (name.Length > 35)
            {
                _logger.LogWarning($"GetProfilesByName() method failed because parameter name length = {name.Length} while it should be below 35");
                throw new ArgumentException("Name can be at most 35 characters long");
            }
                   
          
            List<Profile> profiles = mCache.getCache<List<Profile>>(name);
            
            if (profiles == null)
            {
                _logger.LogDebug($"There is no cache of parameter name {name}");
                profiles = this.repository.GetContactsByName(name);
                mCache.setCache<List<Profile>>(profiles, name);
            }
            else
            {
                _logger.LogDebug($"There is a cache of parameter name {name}");
            }    

            List<PotentialContactView> contacts = new List<PotentialContactView>();
            foreach (Profile item in profiles.ToList())
            {
                if (item.profileId == profileId)
                {
                    profiles.Remove(item);
                }
            }
                for (int i = 0; i < profiles.Count; i++)
                {
                    Profile profile = profiles[i];
                    PotentialContactView contact = new PotentialContactView(profile.displayName, profile.profileId, profile.profileImage);
                    contacts.Add(contact);
                }

                return contacts;

         }
    }
}

