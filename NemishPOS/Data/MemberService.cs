using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NemishPOS.Data

{
    public class MemberService
    {

        public static void SaveAllMembers(List<Membership> members)
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();
            string appMembersFilePath = Utils.GetAppMembersFilePath();
            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }
            var json = JsonSerializer.Serialize(members);
            File.WriteAllText(appMembersFilePath, json);
        }

        public static List<Membership> AddMember(string Username, string PhoneNum)
        {
            List<Membership> members = GetAllMembers();
            members.Add(
            new Membership
            {
                ID = members.Count() + 1,
                MemberName = Username,
                MemberPhoneNum = PhoneNum,
            }
        );
            SaveAllMembers(members);
            return members;
        }

        // Get all members if the file exists else return empty list
        public static List<Membership> GetAllMembers()
        {
            string membersFilePath = Utils.GetAppMembersFilePath();
            if (!File.Exists(membersFilePath))
            {
                return new List<Membership>();
            }

            var json = File.ReadAllText(membersFilePath);

            return JsonSerializer.Deserialize<List<Membership>>(json);
        }



        public static List<Membership> UpdateMembers(int ID, string Username, string PhoneNum)
        {
            List<Membership> members = GetAllMembers();
            Membership membersToUpdate = members.Find(x => x.ID == ID);
            members.Remove(membersToUpdate);
            members.Add(
            new Membership
            {
                ID = ID,
                MemberName = Username,
                MemberPhoneNum = PhoneNum,
            });
            SaveAllMembers(members);
            return members;
        }

        public static List<Membership> DeleteMembers(int ID)
        {
            List<Membership> members = GetAllMembers();
            Membership membersToDelete = members.Find(x => x.ID == ID);

            if (membersToDelete == null)
                throw new Exception("Members is not found");

            members.Remove(membersToDelete);
            SaveAllMembers(members);
            return members;
        }

        public bool MemberExists(string memberPhoneNum)
        {
            List<Membership> members = GetAllMembers();

            // Check if the memberPhoneNum exists in the list of members
            return members.Any(member => member.MemberPhoneNum == memberPhoneNum);

        }

    }
}
