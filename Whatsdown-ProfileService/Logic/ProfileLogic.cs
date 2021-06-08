using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Logic;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Friend_Service.Views;
using Whatsdown_ProfileService.Data;
using Whatsdown_ProfileService.Views;

namespace Whatsdown_ProfileService.Logic
{
    public class ProfileLogic
    {
        private ProfileRepository repository;
        public ProfileLogic(ProfileContext context)
        {
            repository = new ProfileRepository(context);
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

                if (name.Length < 5)
                    throw new ArgumentException("Name has to be at least 5 characters long");

                if (name.Length > 35)
                    throw new ArgumentException("Name can be at most 35 characters long");
                List<string> userIds = new List<string>();
                List<Profile> profiles = this.repository.GetContactsByName(name);
                
               
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

