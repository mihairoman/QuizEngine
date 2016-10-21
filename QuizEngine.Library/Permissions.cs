using System;
using System.Collections.Generic;
using System.Reflection;

namespace QuizEngine.Library
{
    /// <summary>
    /// Class containing global permissions.
    /// </summary>
    public static class Permissions
    {
        public static readonly Guid ManageUsers = new Guid("{2C4A0BE6-D702-460E-A720-BCB44C8F7FB2}");
        public static readonly Guid ManageQuestions = new Guid("{E02880A1-6D81-4C73-A1BD-1E54CB8C6ECC}");
        public static readonly Guid ManageCategories = new Guid("{9D049AE2-6CD6-4219-A329-808D4E8C8193}");
        public static readonly Guid ManageLevels = new Guid("{F5F3C784-684E-4105-97D8-4EAD611F9608}");
        public static readonly Guid ManageTags = new Guid("{75D303A5-9A12-4DC2-9BE4-8EAAD2489834}");
        public static readonly Guid GenerateQuizzes = new Guid("{AEEB2213-C636-4C58-8058-C60201C27E12}");
        public static readonly Guid ScoreQuizzes = new Guid("{fe498b4c-54f1-4db1-a30e-f5d07f4f7a5a}");
        public static readonly Guid QuizHistory = new Guid("{39b7fc12-8a7e-4bc3-9643-d0101d25fd9f}");
        public static readonly Guid AdminDownloadGeneratedTest = new Guid("{6d4194ff-0171-4500-9eff-9c76fcbfabab}");
        public static readonly Guid AdminDownloadPredefinedTest = new Guid("{23f9643f-f284-40c7-87c8-4b40263a5c74}");
        public static readonly Guid FreeTextQuestionsPendingAdminResponse = new Guid("{A6CC7005-4E6D-4224-9A65-2610ED0AD0B3}");

        public static FieldInfo[] generateInterface()
        {
            Type type = typeof(Permissions);
            FieldInfo[] fields = type.GetFields();

            return fields;
        }

        public static List<Guid> GetAllPermissions()
        {
            List<Guid> permissions = new List<Guid>();
            
            foreach (FieldInfo info in generateInterface())
            {
                Guid? permissionUid = (Guid?)info.GetValue(null);
                if (permissionUid.HasValue)
                    permissions.Add(permissionUid.Value);
            }

            return permissions;
        }
    }
}
