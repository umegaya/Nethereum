#!/bin/bash

# library out dir
OUT=$1 


# remove all dlls
rm -r ${OUT}/*.dll 


# copy dependencies
cp src/UnityDotNet4xDeps/*.dll ${OUT}


# build libraries
# order is important. dependency exists
libs=(
	Hex
	Util
	RLP
	Signer
	KeyStore
	ABI
	JsonRpc.Client
	RPC
	Contracts
	Unity
)

for lib in "${libs[@]}" ; do
	make -C src/Nethereum.${lib} NETHEREUM_LIBDIR=../../${OUT}
	if [ $? -ne 0 ]; then
		echo "build ${lib} fails"
		exit $?
	fi
	mv src/Nethereum.${lib}/Nethereum.${lib}.dll ${OUT}
done
