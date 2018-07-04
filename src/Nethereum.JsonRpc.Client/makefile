NETHEREUM_LIBDIR=../../../../dokyogames/nch/client/Assets/Ext/Nethereum.Unity
UNITY_LIBDIR=/Applications/Unity/PlaybackEngines/iOSSupport/Managed
LIBDIR=$(UNITY_LIBDIR),$(NETHEREUM_LIBDIR)
DLLNAME=Nethereum.JsonRpc.Client.dll

all:
	mcs -lib:$(LIBDIR) -r:UnityEngine.CoreModule.dll -r:UnityEngine.UnityWebRequestModule.dll -r:Newtonsoft.Json.dll -r:Nethereum.Hex.dll -r:Nethereum.Util.dll -r:System.Numerics -r:Common.Logging.Core -target:library -out:$(DLLNAME) *.cs */*.cs

