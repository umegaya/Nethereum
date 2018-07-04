NETHEREUM_LIBDIR=../../../../dokyogames/nch/client/Assets/Ext/Nethereum.Unity
UNITY_LIBDIR=/Applications/Unity/PlaybackEngines/iOSSupport/Managed
LIBDIR=$(UNITY_LIBDIR),$(NETHEREUM_LIBDIR)
DLLNAME=Nethereum.Unity.dll

all:
	mcs -lib:$(LIBDIR) -r:UnityEngine.CoreModule.dll -r:UnityEngine.UnityWebRequestModule.dll -r:Newtonsoft.Json.dll -r:Nethereum.Hex.dll -r:Nethereum.Util.dll -r:Nethereum.Signer.dll -r:Nethereum.RPC.dll -r:Nethereum.JsonRpc.Client.dll -r:System.Numerics -target:library -out:$(DLLNAME) *.cs */*.cs

