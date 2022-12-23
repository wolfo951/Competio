namespace API_Tournament.Models
{
    public partial class User
    {
        public string GetRoleName()
        {
            return this.Role;
            //switch(this.Role)
            //{
            //    case 0:
            //        return "User";
            //    case 1:
            //        return "Moderator";
            //    case 2:
            //        return "Admin";
            //    default:
            //        return "RoleNotFound";
            //}
        }
    }
}
