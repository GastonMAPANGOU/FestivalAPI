sp_configure 'show advanced options', 1;  
RECONFIGURE;
GO 
sp_configure 'Ad Hoc Distributed Queries', 1;  
RECONFIGURE;  
GO
-- Remplacer ici TestBDTN par le nom de votre projet
USE [FestivalAPIContext-adf8fbfd-4c0b-47bf-a2e7-41bddfef421b];
GO
SELECT * INTO Country_info
--changer le chemin du fichier selon la machine
FROM OPENROWSET('Microsoft.ACE.OLEDB.12.0',
    'Excel 12.0; Database=C:\Users\mngc9\source\repos\FestivalAPI\FestivalAPI\docfestival.xlsx', [AC1$]);
    
GO

--Pays
insert into pays(nom)
select distinct country_info.pays
from country_info
where country_info.pays  not in (select pays.nom from pays );

GO

--Régions
insert into region(Nom,PaysId)
select distinct country_info.nom_region,pays.Id
from country_info,pays
where pays.nom =  country_info.pays
AND country_info.nom_region  not in (select region.nom from region );

GO

--Departements
insert into departement(Nom,RegionId)
select distinct country_info.nom_departement,region.Id
from country_info,region
where region.nom = country_info.nom_region
AND country_info.nom_departement  not in (select departement.nom from departement);

GO

--Lieux
insert into lieu(Commune,DepartementId)
select distinct country_info.nom_commune,departement.Id
from country_info,departement
where departement.nom = country_info.nom_departement
AND country_info.nom_commune  not in (select lieu.commune from lieu);

GO
DROP TABLE country_info;
GO

