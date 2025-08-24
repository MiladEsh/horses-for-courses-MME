# Horses For Courses
### Api
```bash
POST /coaches
```
#### Request JSON:
```json
{
    "name": "Alice",
    "email": "alice@example.com"
  }
```
### Domain
Coach name cannot be empty.  
Coach email cannot be empty.  
### Data
The database assigns the id.  
The coach is stored in the database.  
Name and email are taken from the request data.  
### Data
### Api
```bash
POST /coaches/{id}/skills
```
#### Request JSON:
```json
{ "skills": ["C#", "Agile"] }
```
### Domain
