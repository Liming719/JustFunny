# JustFunny

本專案使用ASP.NET Core MVC建立，版本為.NET 6

# 功能簡介

主要實作的功能為: 老師身分之使用者可匯入題目，而學生身分之使用者可以進行答題

# 技術簡介

實作UserService、QuestionService兩個資料服務，透過Entity Framework Core操作Local Database
UserService與QuestionService皆繼承抽象類別DataService與IDataService介面
並以IDataService介面注入

匯入題目功能以ClosedXML實作匯入Excel功能，並於匯入後由QuestionService保存
