#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Tls {
    public abstract class SrtpProtectionProfile {
        /*
         * RFC 5764 4.1.2.
         */
        public const int SRTP_AES128_CM_HMAC_SHA1_80 = 0x0001;
        public const int SRTP_AES128_CM_HMAC_SHA1_32 = 0x0002;
        public const int SRTP_NULL_HMAC_SHA1_80 = 0x0005;
        public const int SRTP_NULL_HMAC_SHA1_32 = 0x0006;

        /*
         * RFC 7714 14.2.
         */
        public const int SRTP_AEAD_AES_128_GCM = 0x0007;
        public const int SRTP_AEAD_AES_256_GCM = 0x0008;
    }
}
#pragma warning restore
#endif