<h1 align="center"> Operações fake da bolsa de valores </h1>

<p align="justify">  Neste repositório possui 3 projetos: </p>
<p align="justify"> - Projeto CONSOLE 'DataGenerate', ele irá gerar e salvar dados fake de operações de compra e venda da bolsa de valores no arquivo 'DataOperations.json' que poderá ser encontrado na raiz do projeto ou na pasta Bin. </p>
<p align="justify"> - Projeto .Net 5 'FinancasAPI', irá consumir o arquivo gerado no projeto console, este arquivo precisa ser copiando para a pasta raiz da API que disponibilizará 4 endpoints 
'http://localhost:7300/api/v1/Operation' -- 'http://localhost:7300/api/v1/Operation/GetByAccount' -- 'http://localhost:7300/api/v1/Operation/GetByAtivo' -- 'http://localhost:7300/api/v1/Operation/GetByTypeOperations' e o swagger 'http://localhost:7300/swagger/index.html. Neste projeto foi implementado cache IMemoryCache.'</p>
<p align="justify">  - Projeto .Net Framework 4.5 WPF 'FinancasDesktop', neste projeto será consumido os endpoints da API, a cada chamada na API é gerado um Log com o tempo de resposta da API, este Log pode ser encontrado na pasta raiz do projeto 'FinancasDesktop\bin\Debug\Log\LogRequests20210528.txt'. Assim que for realizada a consulta poderá ser exportada para Excel.</p>

<h3 align="center"> Instruções para executar o projeto </h3>

<p align="justify">  

Faça o clone do projeto ou Download, abra no seu visual studio e compile a solução, em seguida verifique na pasta bin do projeto da API se possui o arquivo 'DataOperations.json',
se não existir execute apenas o projeto console 'DataGenerate', em segundos será criado o arquivo na pasta bin do console, copie o arquivo e cole na pasta bin do projeto da API,
na sequência execute o projeto do desktop e a API. Com os dois projetos em execução clique nos botões da aplicação WPF para gerar as consultas.
</p>

<p></p>
<p></p>

![image](https://user-images.githubusercontent.com/37698049/119929883-bfbe2600-bf54-11eb-9412-66b8462c69af.png)

<p></p>
<p></p>

![image](https://user-images.githubusercontent.com/37698049/119930040-1166b080-bf55-11eb-9a5c-1c1eddc37afe.png)

<p></p>
<p></p>

![image](https://user-images.githubusercontent.com/37698049/119930415-c731ff00-bf55-11eb-956d-c5e8a7f02cf1.png)


