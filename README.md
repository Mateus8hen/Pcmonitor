# 🖥️ PC Monitor Bot

Um utilitário leve e autônomo desenvolvido em C# e .NET que monitora a inicialização do computador e envia notificações automáticas para o seu Telegram sempre que o sistema for ligado ou um usuário fizer login.

---

## ✨ Funcionalidades

- **Notificações em Tempo Real:** Envia data, hora, nome da máquina e usuário ativo diretamente para o seu Telegram.
- **Assistente de Primeira Execução:** Configuração interativa no console na primeira vez em que é aberto.
- **Persistência Local Segura:** Salva credenciais localmente em `%LocalAppData%\PcMonitor`, mantendo o repositório 100% livre de dados sensíveis.
- **Inicialização Automática com o Windows:** Registra o binário na chave `CurrentVersion\Run` do usuário sem necessidade de ferramentas de terceiros.
- **Suporte a Executável Único:** Pode ser compilado como um único arquivo `.exe` autossuficiente (*self-contained*).

---

## 🛠️ Tecnologias Utilizadas

- [C#](https://learn.microsoft.com/dotnet/csharp/)
- [.NET](https://dotnet.microsoft.com/)
- [Telegram Bot API](https://core.telegram.org/bots/api)
- API de Registro do Windows (`Microsoft.Win32.Registry`)

---

## 📋 Pré-requisitos (Configuração do Telegram)

Antes de executar a aplicação pela primeira vez, você precisará de um **Bot Token** e do seu **Chat ID**:

1. **Criar o Bot:**
   - No Telegram, inicie uma conversa com o [@BotFather](https://t.me/BotFather).
   - Envie `/newbot`, defina um nome e um username para ele.
   - Copie o **Token** gerado.
2. **Ativar o Bot (Importante):**
   - Abra a conversa com o bot que você acabou de criar e clique em **Iniciar** (`/start`). *Sem esse passo, a API do Telegram recusará os envios.*
3. **Obter seu Chat ID:**
   - Inicie uma conversa com o [@userinfobot](https://t.me/userinfobot).
   - Copie o número exibido no campo `Id`.

---

## 🚀 Como Usar

### Opção 1: Executando o Código-Fonte

1. Clone o repositório:
   ```bash
   git clone [https://github.com/Mateus8hen/Pcmonitor/edit/main/README.md]
   cd Pcmonitor

   Execute o projeto via .NET CLI:

2. Execute o projeto via .NET CLI:
   ```bash
    dotnet run
 Na primeira execução, informe o Token, o Chat ID e confirme se deseja que o programa inicie junto com o Windows.

### Opção 2: Baixar o .EXE dentro de Release no Github

## 🔄 Como Redefinir as Configurações
Caso precise trocar de bot, alterar o Chat ID ou reconfigurar a inicialização automática:

Pressione Win + R.

Digite %localappdata%\PcMonitor e pressione Enter.

Exclua ou edite o arquivo config.json.

Execute o programa novamente para abrir o assistente inicial.

## 🔒  Segurança
Nenhum token ou identificador pessoal é incluído no código-fonte. O arquivo de configuração local é criado apenas durante o runtime na máquina do usuário e está fora do controle de versão via .gitignore.

## 📄 Licença
Este projeto está sob a licença MIT. Consulte o arquivo LICENSE para obter mais detalhes.

---

> 📌 **Nota sobre Próximas Atualizações (Roadmap):**  
> Atualmente o projeto utiliza a API do Telegram como canal de entrega padrão devido à sua estabilidade e facilidade de integração via bot. Futuramente, pretendem-se realizar testes para expandir o suporte de notificações para outras plataformas de comunicação, tais como **WhatsApp** (via API/Webhooks), **Discord** e **Slack**.
