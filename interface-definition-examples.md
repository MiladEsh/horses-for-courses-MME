# Horses For Courses: Web API Examples

## Coach Endpoints

### POST `/coaches`

**Request (JSON):**

```json
{
  "name": "Alice",
  "email": "alice@example.com"
}
```

**Response:**
Returns the new coach ID (just an integer), f.i. : `42`

### POST `/coaches/{id}/skills`

**Request:**

* URL parameter: `id` (integer)
* JSON body:

```json
{
  "skills": ["C#", "Agile"]
}
```

**Response:**
No content.

### GET `/coaches`

**Response (JSON):**

```json
[
  {
    "id": 1,
    "name": "Alice",
    "email": "alice@example.com",
    "numberOfCoursesAssignedTo": 3
  }
]
```

### GET `/coaches/{id}`

**Request:**

* URL parameter: `id` (integer)

**Response (JSON):**

```json
{
  "id": 1,
  "name": "Alice",
  "email": "alice@example.com",
  "skills": ["C#", "Agile"],
  "courses": [
    { "id": 10, "name": "Advanced C#" },
    { "id": 12, "name": "Test-Driven Development" }
  ]
}
```

## Course Endpoints

### POST `/courses`

**Request (JSON):**

```json
{
  "name": "Advanced C#",
  "startDate": "2025-09-01",
  "endDate": "2025-09-05"
}
```

**Response:**
Returns the new Course ID (just an integer).

### POST `/courses/{id}/skills`

**Request:**

* URL parameter: `id` (integer)
* JSON body:

```json
{
  "skills": ["C#", "LINQ"]
}
```

**Response:**
No content.

### POST `/courses/{id}/timeslots`

**Request:**

* URL parameter: `id` (integer)
* JSON body:

```json
{
  "timeslots": [
    { "day": "Monday", "start": 9, "end": 12 },
    { "day": "Tuesday", "start": 13, "end": 17 }
  ]
}
```

**Response:**
No content.

### POST `/courses/{id}/confirm`

**Request:**

* URL parameter: `id` (integer)

**Response:**
No content.

### POST `/courses/{id}/assign-coach`

**Request:**

* URL parameter: `id` (integer)
* JSON body:

```json
{
  "coachId": 42
}
```

**Response:**
No content.

### GET `/courses`

**Response (JSON):**

```json
[
  {
    "id": 10,
    "name": "Advanced C#",
    "startDate": "2025-09-01",
    "endDate": "2025-09-05",
    "hasSchedule": true,
    "hasCoach": false
  }
]
```

### GET `/courses/{id}`

**Request:**

* URL parameter: `id` (integer)

**Response (JSON):**

```json
{
  "id": 10,
  "name": "Advanced C#",
  "startDate": "2025-09-01",
  "endDate": "2025-09-05",
  "skills": ["C#", "LINQ"],
  "timeslots": [
    { "day": "Monday", "start": 9, "end": 12 },
    { "day": "Tuesday", "start": 13, "end": 17 }
  ],
  "coach": { "id": 42, "name": "Alice" }
}
```
