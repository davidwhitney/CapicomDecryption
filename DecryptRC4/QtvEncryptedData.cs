using System;
using System.Reflection;

namespace Capicom
{
    public class QtvEncryptedData
    {
        // Fields
        private object m_comObject = Activator.CreateInstance(Type.GetTypeFromProgID("CAPICOM.EncryptedData", true));

        // Methods
        public void Decrypt(string encryptedMessage)
        {
            this.m_comObject.GetType().InvokeMember("Decrypt", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, new object[] { encryptedMessage });
        }

        public string Encrypt(EncodingType encodingType)
        {
            return (string)this.m_comObject.GetType().InvokeMember("Encrypt", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, new object[] { encodingType });
        }

        public void SetSecret(string newVal, SecretType secretType)
        {
            this.m_comObject.GetType().InvokeMember("SetSecret", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, new object[] { newVal, secretType });
        }

        // Properties
        public Algorithm Algorithm
        {
            get
            {
                return new Algorithm(this.m_comObject.GetType().InvokeMember("Algorithm", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, null));
            }
        }

        public string Content
        {
            get
            {
                return (string)this.m_comObject.GetType().InvokeMember("Content", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, null);
            }
            set
            {
                this.m_comObject.GetType().InvokeMember("Content", BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, new object[] { value });
            }
        }
    }
}