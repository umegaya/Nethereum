NETHEREUM_LIBDIR=../../../../dokyogames/nch/client/Assets/Ext/Nethereum.Unity
UNITY_LIBDIR=/Applications/Unity/PlaybackEngines/iOSSupport/Managed
LIBDIR=$(UNITY_LIBDIR),$(NETHEREUM_LIBDIR)
DLLNAME=Nethereum.RLP.dll

all:
	mcs -lib:$(LIBDIR) -r:UnityEngine.CoreModule.dll -r:Nethereum.Util.dll -r:Nethereum.Hex.dll -r:Newtonsoft.Json.dll -r:BouncyCastle.Crypto.dll -r:System.Numerics -target:library -out:$(DLLNAME) */*.cs *.cs
