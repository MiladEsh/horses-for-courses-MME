# Horses For Courses
## Api
```bash
POST /coaches
```
### Request JSON:
```json
{
    "name": "Alice",
    "email": "alice@example.com"
  }
```
## Domain
Coach name cannot be an empty string.  
Coach email cannot be an empty string.  
## Data
The coach is stored in the database.  
The database assigns the id.  
Name and email are taken from the request data.  
## Api
```bash
POST /coaches/{id}/skills
```
### Request JSON:
```json
{ "skills": ["C#", "Agile"] }
```
