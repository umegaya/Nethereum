NETHEREUM_LIBDIR=../../../../dokyogames/nch/client/Assets/Ext/Nethereum.Unity
UNITY_LIBDIR=/Applications/Unity/PlaybackEngines/iOSSupport/Managed
LIBDIR=$(UNITY_LIBDIR),$(NETHEREUM_LIBDIR)
DLLNAME=Nethereum.Util.dll

all:
	mcs -lib:$(LIBDIR) -r:UnityEngine.CoreModule.dll -r:Nethereum.Hex.dll -r:Newtonsoft.Json.dll -r:System.Numerics -r:BouncyCastle.Crypto.dll -target:library -out:$(DLLNAME) *.cs */*.cs

