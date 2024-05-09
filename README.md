# Off Duty School Bus

## Description
The Off-duty School Bus project is a platform designed to provide companies with convenient and reliable transportation solutions for their employees during holidays and off-duty periods.

## Techs:

- C#/.Net Core 8
- Sql Server
- Docker
- Documented with Swagger

## Features:

An overview of the Off Duty Bus API.

> **Note:** This is not all the API endpints but the most important features.

- **Authentication & Authorization:**
    - Identity for authentication
    - Signup
    - Login

- **User Management:**
    - Roles
        - _Role 1:_ SchoolTransportationManager
        - _Role 2:_ CompanyTransportationManager
    - Permissions
        - _Permission 1:_ Create, Read, Update, Delete, List School Vehicles
        - _Permission 2:_ Create, Read, Update, Delete, List Vehicle Brands
        - _Permission 3:_ Create, Read, Update, Delete, List Off Duty Buses
        - etc

- **Vehicle Brands:**
    - Create Brand
    - Get Brand
    - Update Brand
    - Delete Brand

- **School Vehicles:**
    - Create School Vehicle
    - Get School Vehicle
    - Update School Vehicle
    - Delete School Vehicle

## About the APP:
- **Architecture Pattern:**

  the project is built using the Clean Architecture pattern, which is a software design pattern that separates the application into three main layers: the presentation layer, the domain layer, and the data layer.

- **Code Style:**
  
    Code Style with CSharpier.

- **Multi-Language:**

  This project is designed to support multiple languages for dynamic content in the database, such as Vehicle Brands and others. The implementation is customized, involving the creation of a new table for each entity that requires multi-language support. Each of these tables includes a language code and value.

> Give it a star if you like it ⭐

## Contact me:
<ul>
  <li>
    <a href="https://www.linkedin.com/in/saeed-eldeeb/" target="_blank" >LinkedIn</a>
  </li>
  <li>
    <a href="mailto:saeedeldeeb1@gmail.com">Email</a>
  </li>
  <li>
    <a href="https://dev.to/saeedeldeeb" target="_blank" >Blog (dev.to)</a>
  </li>
</ul>
