    Dapper-WF
    Created: 19.08.2022
    Created by: Andreas Kowol
     
    Windows Forms application using Dapper or Entity Framework to connect to SQL database.
    The SQL script to restore the database can be found in the file Dapper-WF.sql file
    
    To choose the right ORM customize the usings in the Mainform.cs and Factory.cs files. 
    This is also a great example of how using dependency inversion as one of the SOLID principles allows you to quickly change the database provider or ORM to support it. The program.cs, design and MainForm code itself remain the same. Only the database model and the way it is handled by the ORM changes. 
