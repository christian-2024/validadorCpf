# Azure Function: Validador de CPF em C#

📄 Visão Geral do Projeto
Este projeto implementa uma API serverless simples no Azure Functions, escrita em C#, com o objetivo de validar o Cadastro de Pessoa Física (CPF) brasileiro.
Utilizando o padrão HTTP Trigger, a função recebe um número de CPF via requisição HTTP e aplica o algoritmo padrão de cálculo e validação dos dígitos verificadores, retornando o status de validade.

## 🌟 Principais 

•	Validação Algorítmica: Implementação do algoritmo matemático para cálculo e checagem dos dois dígitos verificadores do CPF.
•	Tecnologia: Desenvolvido em C# (preferencialmente utilizando o Worker de Processo Isolado para maior flexibilidade).
________________________________________
## 💻 Tecnologias Utilizadas

•	Linguagem: C# (ou F#)
•	Framework: .NET Core (ou .NET)
•	Serviço de Nuvem: Azure Functions (HTTP Trigger)
•	Ferramentas: Azure Functions Core Tools (func CLI)
________________________________________
## 🚀 Como Executar Localmente

Para testar e depurar a função no seu ambiente local, você precisará ter o .NET SDK e o Azure Functions Core Tools instalados.

1. Clonar o Repositório
Bash
git clone [URL-DO-SEU-REPOSITÓRIO]
cd [NOME-DA-PASTA-DO-PROJETO]
cd httpValidaCpf # Navegue para o diretório que contém o .csproj
2. Iniciar a Função
1.	Compile o projeto para garantir que todos os artefatos estejam prontos:
Bash
dotnet build
2.	Inicie o host do Functions localmente, apontando para o diretório de saída dos binários:
Bash
### ATENÇÃO: Verifique a versão exata do .NET que você está usando (ex: net8.0)
func start 
Após a inicialização, o console mostrará o endpoint HTTP da sua função, geralmente algo como: http://localhost:7071/api/fnValidarCpf.
________________________________________
## 🛠️ Como Testar a API

A função espera o CPF a ser validado via Query Parameter (na URL) ou no corpo da requisição (JSON).
Exemplo de Requisição (GET)
Use o seu navegador ou uma ferramenta como Postman.
Método	URL
GET	http://localhost:7071/api/fnValidarCpf?cpf=12345678900
Exemplo de Resposta (JSON)
✅ CPF Válido:
JSON
{
  "cpf": "123.456.789-00"
}
❌ CPF Inválido:
JSON
{
  "cpf": "111.111.111-11",
  "mensagem": "CPF inválido."
}
 
________________________________________
## ☁️ Configuração e Criação da Infraestrutura no Azure (Via VS Code)

O método mais rápido para criar e implantar um Azure Function App diretamente da sua máquina local é utilizando a extensão oficial do Azure Functions para VS Code.
1. Pré-requisitos e Extensão
1.	Instale o Visual Studio Code.
2.	Instale a Extensão do Azure Functions: Procure por Azure Functions na aba de Extensões do VS Code e instale.
3.	Faça Login no Azure: Na barra lateral do VS Code (o ícone do Azure), clique em "Sign in to Azure" para autenticar sua conta Microsoft.
2. Criação do Function App no Azure
Utilizaremos a Paleta de Comandos (Ctrl + Shift + P ou Cmd + Shift + P) para iniciar o deploy da infraestrutura:
Comando	Descrição
Ctrl + Shift + P	Abre a Paleta de Comandos.
Digite: Azure Functions: Create Function App in Azure...	Selecione este comando para iniciar o assistente de criação.
Em seguida, siga os prompts na ordem, preenchendo as informações solicitadas:
Prompt	Ação/Exemplo	Observação
Subscription (Assinatura)	Selecione sua conta/assinatura do Azure.	
Enter a globally unique name for the new function app	Exemplo: coappvalidatorcpf001	Este nome será o URL público da sua API. Deve ser único globalmente.
Select a runtime stack	Selecione o runtime do seu projeto (Ex: .NET ou .NET Isolated).	Deve corresponder à versão do seu projeto C#.
Select a location for new resources	Escolha a região do Azure (Ex: East US, South Brazil).	
💡 Atenção: Ao completar o assistente, o VS Code automaticamente provisionará no Azure:
1.	Um Resource Group (Grupo de Recursos).
2.	Um Storage Account (Conta de Armazenamento).
3.	Um Function App (o serviço que executará seu código).
3. Finalizando a Configuração (Se Necessário)
Se você precisar usar o terminal local para comandos do Azure CLI, a autenticação deve ser feita assim:
1.	Login Padrão:
Bash
az login
Isso abrirá uma janela do navegador para você se autenticar.
2.	Solução de Erro de MFA/Tenant (Caso o Login Padrão Falhe):
Se o az login falhar com o erro AADSTS50076 (MFA obrigatória) ou se você tiver múltiplos tenants (diretórios) e precisar especificar um:
Bash
### Use o ID do seu tenant (Diretório) para forçar o login no contexto correto.
az login --tenant NUMERO AQUI DE SUA ASSINATURA

📘 Tenant (ou Diretório): É o seu diretório do Microsoft Entra ID. No Azure CLI, o az login lista as assinaturas que seu usuário tem acesso naquele tenant.
Após o login ser bem-sucedido no terminal, você pode continuar com os comandos de deploy manual.

