using System.Text;
using UnityEngine;

namespace uGaMa.Utils
{
    public static class PixegaUtils
    {
        public static string EncryptDecrypt(string str)
        {
            StringBuilder inSb = new StringBuilder(str);
            StringBuilder outSb = new StringBuilder(str.Length);

            char c;
            for (int i = 0; i < str.Length; i++)
            {
                c = inSb[i];
                c = (char)(c ^ 129);
                outSb.Append(c);
            }

            return outSb.ToString();
        }

        public static byte[] GetByteString (string str)
        {
            byte[] byteStr = Encoding.UTF8.GetBytes(str);

            return byteStr;
        }

        /// <summary>
        /// Gets or add a component. Usage example:
        /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>(gameobject);
        /// </summary>
        public static T GetOrAddComponent<T>(GameObject child) where T : Component
        {
            T result = child.GetComponent<T>();
            if (result == null)
            {
                result = child.AddComponent<T>();
            }
            return result;
        }


        /// <summary>
        /// Remove a component. Usage example:
        /// transform.RemoveComponent<BoxCollider>(gameobject);
        /// </summary>
        internal static void RemoveComponent<T>(GameObject gameObject) where T : Component
        {
            T result = gameObject.GetComponent<T>();
            if(result != null)
            {
                GameObject.Destroy(result);
            }
        }
    }
}