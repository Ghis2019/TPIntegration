Ce travail vise à interroger une API pour récupérer des données de population pour chaque commune à partir d'un code INSEE, puis à mettre à jour une base de données SQL Server avec ces informations de population. Pour ce faire, nous avons commencé par créer une base de données dans laquelle nous avons intégré mes code des communes , les codes postaux et les villes. Nous avons maintenant à l'aide du language de programmation C# établi une connexion avec cette base de données en utilisant l'utisateur par défaut de Windows afin de récupérer les données des populations notamment le nombre pour chaque commune à partir du code INSEE et mettre à jour notre table commune précedemment crée.

Pour mnenner à bien ce travail, nous avons commencer par créer notre Repo, cloner le repo Integrat mise à notre disposition par le professeur et fais des modidications nécesaaire. 

J'ai ajouté la sauvégarde INSE.bak  de la base de donnés au dossier