using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_ProfileService.Data
{
    public class ProfileRepository
    {
        private ProfileContext context;
        public ProfileRepository(ProfileContext context)
        {
            this.context = context;
        }

    

        public Profile GetProfileByProfileId(string ProfileID)
        {
            return context.Profiles.SingleOrDefault<Profile>(p => p.profileId == ProfileID);
        }
        public List<Profile> GetProfiles(List<string> ids)
        {
            return this.context.Profiles.Where(profile => ids.Contains(profile.profileId)).ToList();
        }

        public List<Profile> GetListOfProfilesFromListOfIds(List<string> ids)
        {
            return this.context.Profiles.Where(l => ids.Any(id => id == l.profileId)).ToList();
        }

        public void saveProfile(Profile userProfile)
        {
            context.Profiles.Add(userProfile);
            context.SaveChanges();
        }

        public List<Profile> GetContactsByName(string name)
        {
            return this.context.Profiles.Where(c => c.displayName.Contains(name)).Take(100).ToList();
        }
    }
}
