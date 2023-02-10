using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamarinEcreo.Models
{
    public static class Constants
    {
        public static string UserRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/User/{0}" : "http://localhost:8127/User/{0}";

        public static string AbsenceRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Absence/{0}" : "http://localhost:8127/Absence/{0}";

        public static string AuthenticationRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Authentication/{0}" : "http://localhost:8127/Authentication/{0}";

        public static string ImageRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Media/{0}" : "http://localhost:8127/Media/{0}";
        public static string AdminstratorGetUserInfoRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/GetUserInformation/{0}" : "http://localhost:8127/Media/{0}";
        public static string AdminstratorGetUserPasswordRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/GetPassword/{0}" : "http://localhost:8127/Media/{0}";
        public static string AdminstratorGetUserRoleRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://206.189.49.197/Administrator/GetPassword/{0}" : "http://localhost:8127/Media/{0}";



        //public static string UserRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8127/User/{0}" : "http://localhost:8127/User/{0}";
        //public static string AbsenceRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8127/Absence/{0}" : "http://localhost:8127/Absence/{0}";
        //public static string AuthenticationRestUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:8127/Authentication/{0}" : "http://localhost:8127/Authentication/{0}";

        public static string AuthenticationKey = "authentication key";
    }
}
