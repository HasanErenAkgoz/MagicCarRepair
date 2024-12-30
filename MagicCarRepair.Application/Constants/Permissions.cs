public static class Permissions
{
    public static class Users
    {
        public const string Create = "Users.Create";
        public const string UpdateSelf = "Users.Update.Self";
        public const string UpdateAll = "Users.Update.All";
        public const string ViewSelf = "Users.View.Self";
        public const string ViewAll = "Users.View.All";
        public const string Delete = "Users.Delete";
    }

    public static class Products
    {
        public const string Create = "Products.Create";
        public const string Update = "Products.Update";
        public const string Delete = "Products.Delete";
        public const string View = "Products.View";
    }

    public static class Orders
    {
        public const string Create = "Orders.Create";
        public const string UpdateOwn = "Orders.Update.Own";  
        public const string UpdateAll = "Orders.Update.All"; 
        public const string ViewOwn = "Orders.View.Own";
        public const string ViewAll = "Orders.View.All";
        public const string Delete = "Orders.Delete";
    }
} 