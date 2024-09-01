#if !BESTHTTP_DISABLE_ALTERNATE_SSL && (!UNITY_WEBGL || UNITY_EDITOR)
#pragma warning disable
using System;
using System.IO;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using Best.HTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace Best.HTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers {
    public class Ed448Signer
        : ISigner {
        private readonly Buffer buffer = new Buffer();
        private readonly byte[] context;

        private bool forSigning;
        private Ed448PrivateKeyParameters privateKey;
        private Ed448PublicKeyParameters publicKey;

        public Ed448Signer(byte[] context) {
            this.context = Arrays.Clone(context);
        }

        public virtual string AlgorithmName {
            get { return "Ed448"; }
        }

        public virtual void Init(bool forSigning, ICipherParameters parameters) {
            this.forSigning = forSigning;

            if (forSigning) {
                this.privateKey = (Ed448PrivateKeyParameters) parameters;
                this.publicKey = null;
            }
            else {
                this.privateKey = null;
                this.publicKey = (Ed448PublicKeyParameters) parameters;
            }

            Reset();
        }

        public virtual void Update(byte b) {
            buffer.WriteByte(b);
        }

        public virtual void BlockUpdate(byte[] buf, int off, int len) {
            buffer.Write(buf, off, len);
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER || UNITY_2021_2_OR_NEWER
        public virtual void BlockUpdate(ReadOnlySpan<byte> input) {
            buffer.Write(input);
        }
#endif

        public virtual byte[] GenerateSignature() {
            if (!forSigning || null == privateKey)
                throw new InvalidOperationException("Ed448Signer not initialised for signature generation.");

            return buffer.GenerateSignature(privateKey, context);
        }

        public virtual bool VerifySignature(byte[] signature) {
            if (forSigning || null == publicKey)
                throw new InvalidOperationException("Ed448Signer not initialised for verification");

            return buffer.VerifySignature(publicKey, context, signature);
        }

        public virtual void Reset() {
            buffer.Reset();
        }

        private class Buffer : MemoryStream {
            internal byte[] GenerateSignature(Ed448PrivateKeyParameters privateKey, byte[] ctx) {
                lock (this) {
                    byte[] buf = GetBuffer();
                    int count = Convert.ToInt32(Length);

                    byte[] signature = new byte[Ed448PrivateKeyParameters.SignatureSize];
                    privateKey.Sign(Ed448.Algorithm.Ed448, ctx, buf, 0, count, signature, 0);
                    Reset();
                    return signature;
                }
            }

            internal bool VerifySignature(Ed448PublicKeyParameters publicKey, byte[] ctx, byte[] signature) {
                if (Ed448.SignatureSize != signature.Length) {
                    Reset();
                    return false;
                }

                lock (this) {
                    byte[] buf = GetBuffer();
                    int count = Convert.ToInt32(Length);

                    byte[] pk = publicKey.GetEncoded();
                    bool result = Ed448.Verify(signature, 0, pk, 0, ctx, buf, 0, count);
                    Reset();
                    return result;
                }
            }

            internal void Reset() {
                lock (this) {
                    int count = Convert.ToInt32(Length);
                    Array.Clear(GetBuffer(), 0, count);
                    SetLength(0);
                }
            }
        }
    }
}
#pragma warning restore
#endif