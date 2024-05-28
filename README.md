# TodoList_ASPNETCore
A To do list app powered in .Net 6 for training

Cahier des charges - Gestionnaire de tâches (To-Do list) en ASP.NET Core C#

Introduction :
Le but de ce document est de définir les exigences fonctionnelles et techniques pour le développement d'une 
application de gestion de tâches (To-Do list) utilisant ASP.NET Core en langage C#.

1. Objectifs :
   - Créer une application web intuitive permettant aux utilisateurs de gérer leurs tâches quotidiennes de manière efficace.
   - Offrir une interface conviviale pour créer, modifier, supprimer et marquer les tâches comme complétées.
   - Fournir des fonctionnalités de catégorisation et de priorisation des tâches.
   - Assurer la sécurité des données des utilisateurs et la protection contre les accès non autorisés.

---------------------------------------------------------------------------------------------------------------------------------------
2. Fonctionnalités :

2.1 Authentification et Autorisation :
   - Permettre aux utilisateurs de s'inscrire, de se connecter et de gérer leur profil.
   - Utiliser l'authentification basée sur les jetons JWT (JSON Web Token) pour sécuriser les endpoints.

2.2 Gestion des Tâches :
   - Création de tâches : Les utilisateurs peuvent ajouter de nouvelles tâches avec un titre, une description, 
   une date d'échéance et une priorité.
   - Modification de tâches : Les utilisateurs peuvent modifier les détails des tâches existantes.
   - Suppression de tâches : Les utilisateurs peuvent supprimer des tâches de leur liste.
   - Marquage des tâches comme complétées : Les utilisateurs peuvent marquer les tâches comme étant terminées.

2.3 Catégorisation et Priorisation :
   - Catégorisation des tâches : Les utilisateurs peuvent attribuer des catégories à leurs tâches pour les organiser efficacement.
   - Priorisation des tâches : Les utilisateurs peuvent définir la priorité de leurs tâches pour les classer par ordre d'importance.

2.4 Vue d'Ensemble :
   - Tableau de bord : Affichage d'une vue d'ensemble des tâches à faire, des tâches complétées et des tâches en retard.
   - Filtrage et Tri : Les utilisateurs peuvent filtrer et trier leurs tâches en fonction de différents critères tels que 
   la date d'échéance ou la priorité.

2.5 Notifications :
   - Notifications par e-mail ou notifications push pour rappeler aux utilisateurs des tâches à venir ou des tâches en retard.
---------------------------------------------------------------------------------------------------------------------------------------

3. Technologies Utilisées :
   - ASP.NET Core MVC pour le développement web.
   - Entity Framework Core pour la gestion des données.
   - SQL Server ou tout autre système de gestion de base de données relationnelle pour le stockage des données.
   - Bootstrap ou tout autre framework CSS pour le design et la mise en page.

4. Contraintes Techniques :
   - L'application doit être développée en utilisant ASP.NET Core C# conformément aux meilleures pratiques de développement.
   - Le code doit être bien structuré, modulaire et maintenable pour permettre une évolutivité future.
   - Assurer la sécurité des données en utilisant des techniques de cryptage et en appliquant les principes de protection contre 
   les failles de sécurité.

Ce cahier des charges servira de guide pour le développement de l'application Gestionnaire de tâches (To-Do list) et 
permettra de s'assurer que toutes les exigences fonctionnelles et techniques sont satisfaites.
