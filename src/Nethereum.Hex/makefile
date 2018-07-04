NETHEREUM_LIBDIR=../../../../dokyogames/nch/client/Assets/Ext/Nethereum.Unity
UNITY_LIBDIR=/Applications/Unity/PlaybackEngines/iOSSupport/Managed
LIBDIR=$(UNITY_LIBDIR),$(NETHEREUM_LIBDIR)
DLLNAME=Nethereum.Hex.dll

all:
	mcs -lib:$(LIBDIR) -r:UnityEngine.CoreModule.dll -r:Newtonsoft.Json.dll -r:System.Numerics -target:library -out:$(DLLNAME) */*.cs */*/*.cs

