using AllOutCoin.Services;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AllOutCoin.Models
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "json_settings_key";
        private const string SettingsKey_Exchange = "json_settings_key_exchange";
        private static readonly string SettingsDefault = string.Empty;
        private static readonly string SettingsDefault_Exchange = string.Empty;
        private static Object exchangesLock = new Object();
        private static List<APILogin> exchanges = null;

        #endregion


        public static string JsonValue
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }
        

        public static async System.Threading.Tasks.Task<List<APILogin>> ExchangeListAsync()
        {
            var x = await LoadData.getRSAProvider();

            if(exchanges != null)
            {
                return exchanges;
            }

            lock (exchangesLock)
            {

                var data = AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault_Exchange);

                if (data != SettingsDefault_Exchange)
                {
                    var paramsX = x.ExportParameters(true);
                    var utfData = Convert.FromBase64String(data);

                    var utf8String = UTF8Encoding.UTF8.GetString(utfData);

                    var exchangeentity = JsonConvert.DeserializeObject<List<APILogin>>(utf8String);

                    return exchangeentity;
                }

                return null;
            }
        }
        public static async Task<bool> SaveExchangesList(List<APILogin> exchanges)
        {
            var x = await LoadData.getRSAProvider();

            lock (exchangesLock)
            {
                var utf8Bytes = UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(exchanges));

                AppSettings.AddOrUpdateValue(SettingsKey, Convert.ToBase64String(utf8Bytes));
                
            }

            return true;
        }



        /*public string Decrypt(string encryptedString, byte[] key, , byte[] keyIV)
        {
            if (string.IsNullOrEmpty(encryptedString) )
                return "";
            
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);

            return reader.ReadToEnd();
        }*/

    }
}

