namespace CoffeeManagement.Constant
{
    public class ApiEndPointConstant
    {
        static ApiEndPointConstant() { 
        
        }

        public const string RootEndPoint = "/Api";
        public const string ApiVersion = "v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class Auth 
        {
            public const string AuthEndpoint = ApiEndpoint + "/auth";
            public const string LoginEndpoint = AuthEndpoint + "/login";
            public const string ChangePasswordEndpoint = AuthEndpoint + "/changepassword";
            public const string RequestRevCodeEndpoint = AuthEndpoint + "/requestrevcode";
            public const string verifyRevCodeEndpoint = AuthEndpoint + "/verifyrevcode";
            public const string RegisterCodeEndpoint = AuthEndpoint + "/register";
            public const string LogoutEndPoint = AuthEndpoint + "/logout";
        }
    
        public static class role
        {
            // use for Post(Create) and GetAll (Roles)
            public const string RolesEndPoint = ApiEndpoint + "/roles";
            // use for GetById,Update,Delete (Role)
            public const string RoleEndPoint = ApiEndpoint + "/role";

            public const string GetRoleEndPoint = RoleEndPoint + "/{id}";
        }

        public static class shift 
        {
            // use fir Post(Create) and GetAll
            public const string ShiftsEndPoint = ApiEndpoint + "/shifts";
            // user for GetById,Update,Delete
            public const string ShiftEndPoint = ApiEndpoint + "/shift";

            public const string GetShiftEndPoint = ShiftEndPoint + "/{id}";

            public const string CreateShiftEndPoint = ShiftsEndPoint + "/create";

            public const string UpdateShiftEndPoint = ShiftEndPoint + "/update";

            public const string DeleteShiftEndPoint = ShiftEndPoint + "/delete";
        }
    }
}
