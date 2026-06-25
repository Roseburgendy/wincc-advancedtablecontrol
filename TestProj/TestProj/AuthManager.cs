using System.Security.Cryptography;
using System.Text;

namespace WINCCReporter
{
    internal static class AuthManager
    {

        public static bool IsAdmin { get; private set; } = false;


        public static bool HasPassword =>
            !string.IsNullOrEmpty(ConfigManager.Config?.AdminPasswordHash);

        /// <summary>
        /// 尝试以指定密码登录管理员账户。若配置中未设置密码，任意输入均视为成功。
        /// </summary>
        public static bool Login(string password)
        {
            string stored = ConfigManager.Config?.AdminPasswordHash ?? "";
            // password not set, any input is valid
            if (string.IsNullOrEmpty(stored))
            {
                IsAdmin = true;
                return true;
            }
            // password set, check hash
            IsAdmin = Hash(password) == stored;
            return IsAdmin;
        }

        public static void Logout() => IsAdmin = false;

        /// <summary>设置或清除管理员密码</summary>
        public static void SetPassword(string newPassword)
        {
            if (ConfigManager.Config == null) return;
            ConfigManager.Config.AdminPasswordHash =
                string.IsNullOrEmpty(newPassword) ? "" : Hash(newPassword);
            ConfigManager.SaveConfig();
        }

        // hash function
        internal static string Hash(string input)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input ?? ""));
                var sb = new StringBuilder();
                foreach (byte b in bytes) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}
