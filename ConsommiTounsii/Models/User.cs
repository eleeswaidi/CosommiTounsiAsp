using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsommiTounsii.Models
{
    public class User
    {

        public int user_id { get; set; }

            public string firstname_user { get; set; }

        public string lastname_user { get; set; }

        public DateTime dateofbirth_name { get; set; }

        public string address_user { get; set; }

        public string email_user { get; set; }

        public string password { get; set; }

        public User()
        {

        }

        public User(int user_id, string firstname_user, string lastname_user, DateTime dateofbirth_name, string address_user, string email_user, string password)
        {
            this.user_id = user_id;
            this.firstname_user = firstname_user;
            this.lastname_user = lastname_user;
            this.dateofbirth_name = dateofbirth_name;
            this.address_user = address_user;
            this.email_user = email_user;
            this.password = password;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   user_id == user.user_id &&
                   firstname_user == user.firstname_user &&
                   lastname_user == user.lastname_user &&
                   dateofbirth_name == user.dateofbirth_name &&
                   address_user == user.address_user &&
                   email_user == user.email_user &&
                   password == user.password;
        }

        public override int GetHashCode()
        {
            int hashCode = 384489457;
            hashCode = hashCode * -1521134295 + user_id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(firstname_user);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lastname_user);
            hashCode = hashCode * -1521134295 + dateofbirth_name.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(address_user);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email_user);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            return hashCode;
        }

        public override string ToString()
        {

            return ("id"+user_id+"firstname"+firstname_user+"lastname"+lastname_user+"date of birth"+dateofbirth_name+"address"+address_user+"email"+email_user+"password"+password);
        }

    }
}
