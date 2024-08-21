# E-commerce Platform Project

![574_1x_shots_so](https://github.com/FacuMasino/tpc-equipo-16/assets/13455216/bab236d2-6d0f-426c-b2b3-b066bd462df0)
![279_1x_shots_so](https://github.com/FacuMasino/tpc-equipo-16/assets/13455216/dcdb0326-5118-4494-a16d-d07cdb6cd041)
![992_1x_shots_so](https://github.com/FacuMasino/tpc-equipo-16/assets/13455216/bd82f11a-a907-4ede-a9fc-383efb0c3910)
![292_1x_shots_so](https://github.com/FacuMasino/tpc-equipo-16/assets/13455216/d363a11b-46d9-409b-bc3c-b5bb7b3bdc0f)

## Overview

&nbsp; This project aims to develop a functional e-commerce platform that enables users to register, log in, browse products, add products to a shopping cart, and place orders. Administrators will have the capability to add new products, update stock, prices, and track shipment statuses. The platform is designed to be intuitive, secure, and efficient in handling data.

## Requirements to Run 

### SQL Server
- Change the desired connection string in DataAccessLayer/SQL/CreateResetDB.bat
- Run the CreateResetDB.bat file to create the database

### Web.Config
- You'll need to create a Web.config file inside of WebForms folder replacing the data for Connection String and Mailtrap API (you can get it free on [mailtrap](https://mailtrap.io)).
Copy and Paste this and replace the mentioned data:
```xml
<?xml version="1.0" encoding="utf-8"?>
<!--
  For more information on how to configure your ASP.NET application, please visit
  https://go.microsoft.com/fwlink/?LinkId=169433
  -->
<configuration>
	<system.web>
		<compilation debug="true" targetFramework="4.8.1" />
		<httpRuntime targetFramework="4.8.1" />
		<pages>
			<namespaces>
				<add namespace="DomainModelLayer" />
				<add namespace="UtilitiesLayer" />
			</namespaces>
		</pages>
	</system.web>
	<system.webServer>
		<defaultDocument>
			<files>
				<!-- Cambia Default.aspx por Home.aspx -->
				<add value="Home.aspx" />
			</files>
		</defaultDocument>
		<staticContent>
			<mimeMap fileExtension=".webmanifest" mimeType="application/manifest+json" />
		</staticContent>
	</system.webServer>
	<location path="Admin">
		<system.webServer>
			<defaultDocument>
				<files>
					<add value="Dashboard.aspx" />
				</files>
			</defaultDocument>
		</system.webServer>
	</location>
	<connectionStrings>
		<!-- CAMBIAR NOMBRE DE CONNECTION STRING EN DataAccessLayer/DataAccess.cs -->
		<add name="DB_ECOMMERCE" connectionString="server=SERVER.ADDRESS.OR.IP; database=DATABASE_NAME; User=;Password=;" />
	</connectionStrings>
	<system.codedom>
		<compilers>
			<compiler language="c#;cs;csharp" extension=".cs" warningLevel="4" compilerOptions="/langversion:default /nowarn:1659;1699;1701;612;618" type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.CSharpCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=4.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
			<compiler language="vb;vbs;visualbasic;vbscript" extension=".vb" warningLevel="4" compilerOptions="/langversion:default /nowarn:41008,40000,40008 /define:_MYTYPE=\&quot;Web\&quot; /optionInfer+" type="Microsoft.CodeDom.Providers.DotNetCompilerPlatform.VBCodeProvider, Microsoft.CodeDom.Providers.DotNetCompilerPlatform, Version=4.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
		</compilers>
	</system.codedom>
	<appSettings>
		<!-- DATOS PARA EMAILING -->
		<add key="api_token" value="Bearer API_DE_MAILTRAP" />
		<add key="email_from" value="EMAIL@TUDOMINIO.com"/>
		<add key="email_name_from" value="NOMBRE REMITENTE"/>
		<add key="ecommerce_name" value="NOMBRE ECOMMERCE"/>
		<add key="ecommerce_url" value="URLECOMMERCE.COM"/>
	</appSettings>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="System.Runtime.CompilerServices.Unsafe" publicKeyToken="b03f5f7f11d50a3a" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-6.0.0.0" newVersion="6.0.0.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
```

## Project Goals

- Provide a user-friendly platform for sellers.
- Facilitate user registration and authentication.
- Manage products, stock, and prices efficiently.
- Implement a shopping cart system and order management.
- Support payment options (e.g., MercadoPago, bank transfers) and shipping logistics.
- Create an accessible and user-friendly interface.

## Features

- User Registration and Authentication
  - Register new users.
  - Log in and manage passwords.
  - User and Administrator roles.

- Shopping Cart
  - Add products to the cart.
  - Display cart summary and total.
  - Edit and remove products from the cart.

- Order and Product Management
  - View order status (e.g., in preparation, shipped, delivered).
  - Option to cancel orders.
  - CRUD (Create, Read, Update, Delete) operations for products.

- Payment Options
  - Integrate payment methods such as MercadoPago, bank transfer, and cash.

- Shipping and Logistics
  - Manage shipping options (home delivery, in-store pickup).
  - Manual logistics management by the admin.

- Additional Features
  - Create and apply discount coupons.
  - Import products from a CSV file.
 
## Desarrolladores

<a href="https://github.com/FacuMasino/tpc-equipo-16/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=FacuMasino/tpc-equipo-16" />
</a>

## License

&nbsp; This is an open source project developed under the [GNU General Public License](https://github.com/FacuMasino/tpc-equipo-16/blob/main/LICENSE).
