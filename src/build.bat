rem az account set --subscription 32886bdb-91b8-4941-96c9-a662977d4455
docker build -t ecsdemos.azurecr.io/pathfinderfx:1.0.0.7 .
docker push ecsdemos.azurecr.io/pathfinderfx:1.0.0.7