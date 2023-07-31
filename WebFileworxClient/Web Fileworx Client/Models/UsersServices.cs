namespace Web_Fileworx_Client.Models
{
    public class UsersServices
    {
        private StreamReader? userReader;
        private StreamWriter? userWriter;
        public List<User> allUsers = new List<User>();
        public User? loggedInUser = null;
        public User SelectedUser = null;

        public void RemoveUser(User user)
        {
            string g = EditBeforeRun.savedusersFiles + "\\" + user.GUID.ToString()+".txt";
            if (File.Exists(g)) 
            {
                File.Delete(g);
            }
        }

        public void AddUser(User user)
        {
            FileStream fs = new FileStream($"{EditBeforeRun.savedusersFiles}" + $"\\{user.GUID}.txt", FileMode.Create, FileAccess.Write);
            userWriter = new StreamWriter(fs);
            userWriter.AutoFlush = true;
            userWriter.WriteLine($"{user.Name}%%$$##{user.UserName}%%$$##{user.Password}%%$$##{user.GUID}%%$$##{user.IsAdmin}");
            userWriter?.Close();
        }

        public void AddAllSavedUsers()
        {
            string[] savedUsersFiles = Directory.GetFiles(EditBeforeRun.savedusersFiles);

            foreach (string file in savedUsersFiles)
            {
                readUserTextFile(file);
            }
        }

        public void RefreshUsers()
        {
            allUsers.Clear();
            AddAllSavedUsers();
        }

        public List<User> GetAllUsers()
        {
            return allUsers;
        }

        public void readUserTextFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                userReader = new StreamReader(fs);
                string? accountRecord = userReader.ReadLine();

                if (accountRecord != null)
                {
                    string[] thisUserArray = accountRecord.Split(EditBeforeRun.Seperator, StringSplitOptions.None);
                    User thisUser = new User(thisUserArray);
                    allUsers.Add(thisUser);
                }
            }
        }

        public User? CheckLogInInfo(string? username, string? password)
        {
            foreach (User user in allUsers)
            {
                if((user.UserName == username) && (user.Password == password))
                {
                    loggedInUser = user;
                    return user;
                }
            }
            return null;
        }

    }
}
