# Para rodar o minikube
- instalar o minikube
- minikube start

# Ativar os recursos
- minikube addons enable metrics-server
- minikube addons enable volumesnapshots
- minikube addons enable csi-hostpath-driver

# Abrir dashboard
- minikube dashboard (opcional)

# Rodar os arquivos yaml
- cd k8s

- kubectl apply -f db-pvc.yaml
- kubectl apply -f db-configmap.yaml
- kubectl apply -f db-service.yaml
- kubectl apply -f db-statefullset.yaml
- kubectl apply -f api-configmap.yaml 
- kubectl apply -f api-service.yaml
- kubectl apply -f api-deployment.yaml 
- kubectl apply -f api-hpa.yaml 


1. Para verificar Logs de um Pod em Execução
Se o pod está em execução e você deseja verificar os logs em tempo real, use a opção -f ou --follow para seguir os logs em tempo real:
kubectl logs -f <nome-do-pod>


2. Para Fazer o port-forward para acessar a aplicação no Windows:
kubectl port-forward service/svc-api-loadbalancer 8080:80 => vai ser possível acessar em http://localhost:8080