using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagerModel
{
    public partial class Login
    {
        public bool login(string username, string password)
        {
            PottersEntities db = new PottersEntities();
            bool LoginOk;
            if (!(string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password)))
            {
                SchoolManagerModel.Login login = db.Logins.Find(username);
                LoginOk = false;
                if (login != null)
                {
                    if (login.Password.Equals(password))
                    {
                        LoginOk = true;
                    }
                }
                return LoginOk;
            }
            else
            {
                return false;
            }
            
        }

        public bool registerAccountUser(string username, string password)
        {
            PottersEntities db = new PottersEntities();

            var registeredUsers = db.Logins.Find(username);
            if (registeredUsers != null)
            {
                return false;
            }
            else
            {
                SchoolManagerModel.Login login = db.Logins.Create();
                login.Id = Convert.ToString(Guid.NewGuid());
                login.Password = password;
                login.Username = username;
                login.PasswordSalt = "salt";

                db.Logins.Add(login);
                db.SaveChanges();

                return true;
            }

        }

        public bool changePassword(string username, string newPassword, string oldPassword)
        {
            PottersEntities db = new PottersEntities();
            bool successful = false;

            var registeredUsers = db.Logins.Find(username);
            if (registeredUsers != null)
            {
                string _oldPassword = oldPassword;
                if (_oldPassword.Equals(registeredUsers.Password))
                {
                    registeredUsers.Password = newPassword;

                    db.Entry(registeredUsers).State = EntityState.Modified;
                    db.SaveChanges();
                    successful = true;
                }
                else
                {
                    successful = false;
                }

            }
            return successful;

        }

        //public bool login(string username, string password)
        //{
        //    PottersEntities db = new PottersEntities();
        //    bool LoginOk;
        //    if (!(string.IsNullOrWhiteSpace(username) && string.IsNullOrWhiteSpace(password)))
        //    {
        //        var simpleCrypto = new SimpleCrypto.PBKDF2();
        //        var encriptPass = simpleCrypto.Compute(password);


        //        SchoolManagerModel.Login login = db.Logins.Find(username);
        //        LoginOk = false;
        //        if (login != null)
        //        {
        //            if (login.Password.Equals(simpleCrypto.Compute(password, login.PasswordSalt)))
        //            {
        //                LoginOk = true;
        //            }
        //        }
        //        return LoginOk;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
        /*//public bool registerAccountUser(string username,string password)
        //{
        //    PottersEntities db = new PottersEntities();
        //    var simpleCrypto = new SimpleCrypto.PBKDF2();
            

        //    var registeredUsers = db.Logins.Find(username);
        //    if (registeredUsers != null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        SchoolManagerModel.Login login = db.Logins.Create();
        //        var encriptPass = simpleCrypto.Compute(password);
        //        login.Id = Convert.ToString(Guid.NewGuid());
        //        login.Password = encriptPass;
        //        login.Username = username;
        //        login.PasswordSalt = simpleCrypto.Salt;

        //        db.Logins.Add(login);
        //        db.SaveChanges();

        //        return true;
        //    }
            
        //}
         * */
        /*public bool changePassword(string username,string newPassword,string oldPassword)
        {
            PottersEntities db = new PottersEntities();
            var simpleCrypto = new SimpleCrypto.PBKDF2();
            bool successful = false;

            var registeredUsers = db.Logins.Find(username);
            if (registeredUsers != null)
            {
                string _oldPassword = simpleCrypto.Compute(oldPassword, registeredUsers.PasswordSalt);
                if (_oldPassword.Equals(registeredUsers.Password))
	            {
                    var encriptPass = simpleCrypto.Compute(newPassword);
                    registeredUsers.Password = encriptPass;

                    db.Entry(registeredUsers).State = EntityState.Modified;
                    db.SaveChanges();
                    successful = true;
	            }
                else
                {
                    successful = false;
                }

            }
            return successful;
            
        }
        */



    }
}
