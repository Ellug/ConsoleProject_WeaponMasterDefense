using Google.Cloud.Firestore;
using System;

namespace WeaponMasterDefense
{
    public static class FirebaseManager
    {
        public static FirestoreDb Db { get; private set; }

        public static void Initialize()
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys", "firestore-access.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

            Db = FirestoreDb.Create("wmdefenceranking");
        }
    }
}