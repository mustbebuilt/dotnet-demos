# .NET Demos

## Lab 01 - Exercise 01: Book Class

- **Overview & Walkthrough:** [lab01exercise01/readme.md](lab01exercise01/readme.md)
- **Class Diagram:** [ClassDiagram.puml](lab01exercise01/ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](lab01exercise01/SequenceDiagram.puml)

### Run from root
```bash
dotnet build lab01exercise01
dotnet run --project lab01exercise01
```

---

## Lab 01 - Exercise 02: Multiple Related Classes (Student & Module)

- **Overview & Walkthrough:** [lab01exercise02/readme.md](lab01exercise02/readme.md)
- **Class Diagram:** [ClassDiagram.puml](lab01exercise02/ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](lab01exercise02/SequenceDiagram.puml)

### Run from root
```bash
dotnet build lab01exercise02
dotnet run --project lab01exercise02
```

### Or navigate into the directory
```bash
cd lab01exercise02
dotnet build
dotnet run
```

---

## Lab 01 - Exercise 03: Aggregation & Object Collaboration (Library Management)

- **Overview & Walkthrough:** [lab01exercise03/readme.md](lab01exercise03/readme.md)
- **Class Diagram:** [ClassDiagram.puml](lab01exercise03/ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](lab01exercise03/SequenceDiagram.puml)

### Run from root
```bash
dotnet build lab01exercise03
dotnet run --project lab01exercise03
```

### Or navigate into the directory
```bash
cd lab01exercise03
dotnet build
dotnet run
```

---

## Lab 01 - Exercise 04: Inheritance & Polymorphism (Employee Hierarchy)

- **Overview & Walkthrough:** [lab01exercise04/readme.md](lab01exercise04/readme.md)
- **Class Diagram:** [ClassDiagram.puml](lab01exercise04/ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](lab01exercise04/SequenceDiagram.puml)

### Run from root
```bash
dotnet build lab01exercise04
dotnet run --project lab01exercise04
```

### Or navigate into the directory
```bash
cd lab01exercise04
dotnet build
dotnet run
```

---

## Lab 01 - Exercise 05: Interfaces & Extensibility (Payment Processing)

- **Overview & Walkthrough:** [lab01exercise05/readme.md](lab01exercise05/readme.md)
- **Class Diagram:** [ClassDiagram.puml](lab01exercise05/ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](lab01exercise05/SequenceDiagram.puml)

### Run from root
```bash
dotnet build lab01exercise05
dotnet run --project lab01exercise05
```

### Or navigate into the directory
```bash
cd lab01exercise05
dotnet build
dotnet run
```

---

## Lab 01 - Exercise 06: Online Shopping System (Comprehensive OOP)

- **Overview & Walkthrough:** [lab01exercise06/readme.md](lab01exercise06/readme.md)
- **Class Diagram:** [ClassDiagram.puml](lab01exercise06/ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](lab01exercise06/SequenceDiagram.puml)

### Run from root
```bash
dotnet build lab01exercise06
dotnet run --project lab01exercise06
```

### Or navigate into the directory
```bash
cd lab01exercise06
dotnet build
dotnet run
```

---

## Appendix: Viewing & Rendering PlantUML (`.puml`) Diagrams

All exercises include UML **Class Diagrams** (`ClassDiagram.puml`) and **Sequence Diagrams** (`SequenceDiagram.puml`) written in [PlantUML](https://plantuml.com/).

### Method 1: In VS Code (Recommended)

1. **Install the Extension:**
   - Open Extensions (`Cmd + Shift + X` on macOS / `Ctrl + Shift + X` on Windows).
   - Search for and install **PlantUML** (by *jebbs*).

2. **Zero-Setup Online Rendering:**
   - The workspace is already pre-configured (`.vscode/settings.json`) to use the official PlantUML web server:
     ```json
     {
       "plantuml.render": "PlantUMLServer",
       "plantuml.server": "https://www.plantuml.com/plantuml"
     }
     ```
   - *No local Java or Graphviz installation is required.*

3. **Preview a Diagram:**
   - Open any `.puml` file.
   - Press **`Option + D`** (macOS) or **`Alt + D`** (Windows/Linux), or run `Cmd + Shift + P` $\rightarrow$ **`PlantUML: Preview Current Diagram`**.

4. **Export as Image (PNG/SVG/PDF):**
   - Press `Cmd + Shift + P` $\rightarrow$ select **`PlantUML: Export Current Diagram`** and choose your preferred format.

---

### Method 2: Offline / Local CLI Rendering

If you prefer local generation without internet access:

1. **Install Java & Graphviz:**
   - **macOS (Homebrew):**
     ```bash
     brew install openjdk graphviz plantuml
     ```
   - **Linux (Ubuntu/Debian):**
     ```bash
     sudo apt install default-jre graphviz plantuml
     ```
   - **Windows (Chocolatey / Winget):**
     ```powershell
     choco install jre8 graphviz plantuml
     ```

2. **Generate PNG Images via CLI:**
   ```bash
   plantuml lab01exercise01/ClassDiagram.puml
   plantuml lab01exercise01/SequenceDiagram.puml
   ```

---

### Method 3: Instant Online Editors (No Installation)

You can copy and paste the contents of any `.puml` file directly into web-based renderers:
- **[PlantText](https://www.planttext.com/)**
- **[PlantUML Official Live Server](https://www.plantuml.com/plantuml/uml/)**