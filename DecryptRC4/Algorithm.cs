using System.Reflection;

namespace Capicom
{
    public class Algorithm
    {
        // Fields
        private object m_comObject;

        // Methods
        public Algorithm(object comObject)
        {
            this.m_comObject = comObject;
        }

        // Properties
        public EncryptionKeyLength KeyLength
        {
            get
            {
                return (EncryptionKeyLength)this.m_comObject.GetType().InvokeMember("KeyLength", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, null);
            }
            set
            {
                this.m_comObject.GetType().InvokeMember("KeyLength", BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, new object[] { value });
            }
        }

        public EncryptionAlgorithm Name
        {
            get
            {
                return (EncryptionAlgorithm)this.m_comObject.GetType().InvokeMember("Name", BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, null);
            }
            set
            {
                this.m_comObject.GetType().InvokeMember("Name", BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance, null, this.m_comObject, new object[] { value });
            }
        }
    }
}