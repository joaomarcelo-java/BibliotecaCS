# BibliotecaCS

📚 Sistema de Biblioteca em C#
Projeto desenvolvido como portfólio para demonstrar conhecimentos sólidos em C#, explorando conceitos de Programação Orientada a Objetos (POO), persistência em Banco de Dados (Entity Framework) e arquitetura de sistemas.

O objetivo principal deste aplicativo é simular a rotina de uma biblioteca através de uma interface de terminal (CLI), permitindo o gerenciamento completo de usuários, livros e empréstimos.

🛠️ Regras de Negócio e Funcionalidades
Prazos: Cada empréstimo tem duração padrão de 2 semanas.

Limites: Cada usuário pode realizar o empréstimo de no máximo 3 livros simultaneamente.

Flexibilidade: A devolução pode ser realizada a qualquer momento antes ou durante o prazo.

Perfis de Acesso:

Usuário Comum: Pode visualizar livros disponíveis, realizar locações e gerenciar suas devoluções.

Bibliotecário: Possui permissões administrativas para adicionar novos títulos, encerrar empréstimos de qualquer usuário e consultar informações detalhadas do sistema.

🏗️ Estrutura das Classes Principais
Classe Livro
Representa a unidade de obra literária no sistema. Além de conter dados como título e autor, ela possui uma propriedade de Link de Acesso.

Nota: O campo Link simula o local onde o usuário teria acesso direto ao conteúdo digital do livro após realizar o empréstimo.

Classe Emprestimo
É a classe que faz a "ponte" entre o Usuário e o Livro. Ela é responsável por gerenciar:

A data exata em que o livro foi retirado.

O cálculo automático da data de devolução.

O vínculo entre o ID do usuário e o exemplar alugado.

🚀 Como Executar o Projeto
Este projeto utiliza SQLite, o que significa que o banco de dados roda localmente na sua máquina sem necessidade de configurações externas complexas.

Pré-requisitos: Certifique-se de ter o .NET SDK instalado.

Clonar o repositório:

Bash

git clone https://github.com/joaomarcelo-java/BibliotecaCS.git
Navegar até a pasta:

Bash

cd BibliotecaCS
Executar a aplicação:

Bash

dotnet run
O sistema verificará automaticamente se o banco de dados existe. Caso não exista, ele será criado no primeiro início.
