# Quick Start Guide - Viewing UML Diagrams

## 🚀 Fastest Way to View (3 Easy Steps)

### Option 1: VS Code (Recommended)

1. **Install VS Code Extension**
   - Open VS Code
   - Press `Ctrl+Shift+X` (Extensions)
   - Search for "PlantUML"
   - Install "PlantUML" by jebbs

2. **Open a Diagram**
   - Navigate to `UML-Diagrams` folder
   - Open any `.md` file (e.g., `ClassDiagram.md`)

3. **Preview**
   - Press `Alt+D` to preview
   - Or right-click → "Preview Current Diagram"

✅ **Done!** The diagram will render in a side panel.

---

### Option 2: Online Viewer (No Installation)

1. **Visit PlantUML Online**
   - Go to: http://www.plantuml.com/plantuml/uml/

2. **Copy Diagram Code**
   - Open any diagram file (e.g., `ClassDiagram.md`)
   - Copy everything between ` ```plantuml` and ` ``` `

3. **Paste & View**
   - Paste into the online editor
   - Diagram renders automatically

✅ **Done!** You can download as PNG/SVG.

---

## 📋 Available Diagrams

### 1. 🏗️ **Class Diagram** - `ClassDiagram.md`
Shows all classes, their properties, methods, and relationships.

**Best for understanding:**
- Application structure
- Class hierarchy (Person → Admin)
- Repository pattern implementation
- Entity relationships

**Key insight:** Application follows layered architecture with clear separation of concerns.

---

### 2. 👤 **Use Case Diagram** - `UseCaseDiagram.md`
Shows what users can do with the system.

**Best for understanding:**
- All available features
- User interactions
- System capabilities
- Functional requirements

**Key insight:** Admin can manage customers, employees, materials, and view analytics.

---

### 3. 🔄 **Sequence Diagram - Login** - `SequenceDiagram-Login.md`
Shows step-by-step login process.

**Best for understanding:**
- Authentication flow
- How Form1 → Admin → DataAccess → Database communicate
- Error handling for invalid credentials

**Key insight:** Login uses database query to validate credentials and returns to dashboard on success.

---

### 4. 🔄 **Sequence Diagram - CRUD** - `SequenceDiagram-CustomerCRUD.md`
Shows all Create, Read, Update, Delete operations.

**Best for understanding:**
- How CRUD operations work
- Data flow from UI to database
- Validation and error handling
- Same pattern for Customers, Employees, Materials

**Key insight:** All CRUD operations follow Repository → DataAccess → Database pattern.

---

### 5. 📦 **Component Diagram** - `ComponentDiagram.md`
Shows high-level system architecture.

**Best for understanding:**
- System layers (Presentation, Business, Data)
- How components interact
- Design patterns used
- Technology stack

**Key insight:** Clear 3-layer architecture: UI → Business Logic → Data Access.

---

### 6. 🔀 **Activity Diagram** - `ActivityDiagram-MaterialManagement.md`
Shows workflow for material management.

**Best for understanding:**
- Business process flow
- Decision points (validation, confirmation)
- Parallel operations (Add/Update/Delete)
- Error handling

**Key insight:** Operations include validation loops and confirmation dialogs for safety.

---

### 7. 🌐 **Deployment Diagram** - `DeploymentDiagram.md`
Shows how application is deployed on hardware.

**Best for understanding:**
- Physical deployment
- Client-Server architecture
- Network communication
- Installation requirements

**Key insight:** Supports both standalone (LocalDB) and multi-user (SQL Server) deployment.

---

### 8. 🔄 **State Diagram** - `StateDiagram-UserSession.md`
Shows user session lifecycle and states.

**Best for understanding:**
- Application states (Login → Dashboard → Management → Logout)
- State transitions
- User navigation flow
- Session management

**Key insight:** Complete session lifecycle from launch to logout with all navigation paths.

---

## 🎯 Which Diagram Should I View First?

### If you're a **Developer**:
1. **Class Diagram** - Understand code structure
2. **Component Diagram** - See architecture
3. **Sequence Diagram (CRUD)** - Learn data flow

### If you're a **Designer/Architect**:
1. **Component Diagram** - See high-level architecture
2. **Deployment Diagram** - Understand infrastructure
3. **Class Diagram** - See design patterns

### If you're a **Business Analyst**:
1. **Use Case Diagram** - See functionality
2. **Activity Diagram** - Understand workflows
3. **State Diagram** - See user journey

### If you're a **Tester/QA**:
1. **Use Case Diagram** - Test scenarios
2. **Activity Diagram** - Test workflows
3. **State Diagram** - Test state transitions
4. **Sequence Diagrams** - Test data flow

### If you're **Learning the Application**:
Start with: **Use Case** → **Component** → **Class** → **Sequence (Login)** → **Activity**

---

## 🛠️ Generate PNG Images (Optional)

### Using PlantUML CLI

1. **Install PlantUML**
   ```bash
   # Download from: https://plantuml.com/download
   # Or use package manager (Windows with Chocolatey):
   choco install plantuml
   ```

2. **Generate All Diagrams**
   ```bash
   cd UML-Diagrams
   plantuml *.md -tpng
   ```

3. **Generate Specific Diagram**
   ```bash
   plantuml ClassDiagram.md -tpng
   ```

4. **Generate as SVG (scalable)**
   ```bash
   plantuml *.md -tsvg
   ```

✅ **Result:** PNG/SVG files created in the same folder.

---

## 🖼️ Export from VS Code

1. Open diagram in VS Code
2. Press `Ctrl+Shift+P`
3. Type "PlantUML: Export"
4. Choose format (PNG, SVG, PDF)
5. Select output location

---

## 📱 View on Mobile/Tablet

### Method 1: GitHub (if pushed to repo)
1. Push diagrams to GitHub
2. Open on mobile browser
3. GitHub renders markdown with PlantUML

### Method 2: Generate PNG first
1. Generate PNG files using CLI
2. Transfer to mobile device
3. View with any image viewer

---

## ❓ Troubleshooting

### Problem: PlantUML extension not rendering

**Solution 1**: Install Graphviz
```bash
# Windows (Chocolatey)
choco install graphviz

# Or download from: https://graphviz.org/download/
```

**Solution 2**: Configure PlantUML server
- VS Code Settings → PlantUML → Server
- Use online server: http://www.plantuml.com/plantuml

---

### Problem: Diagram too small/large

**VS Code:**
- Use zoom: `Ctrl + Mouse Wheel`
- Or: `Ctrl + Plus/Minus`

**Online Editor:**
- Use browser zoom: `Ctrl + Plus/Minus`

**Export:**
- Modify PlantUML code: Add `scale 2` at the top for 2x size

---

### Problem: Syntax errors in diagram

**Check:**
1. All `@startuml` have matching `@enduml`
2. No missing closing braces
3. No special characters without escaping

**Fix:**
- PlantUML will show error message
- Check line number indicated in error

---

## 💡 Tips

### Tip 1: Live Preview in VS Code
- Keep diagram file and preview side-by-side
- Edit and see changes in real-time
- Great for learning PlantUML syntax

### Tip 2: Export All Diagrams as PDF
```bash
plantuml UML-Diagrams/*.md -tpdf
```
Creates a PDF for each diagram - perfect for documentation!

### Tip 3: Include in Documentation
- Export as SVG (scalable, sharp at any size)
- Embed in Word/PowerPoint
- Add to project wiki

### Tip 4: Print for Study
- Export as PNG with high DPI
- Print A3 size for complex diagrams
- Use as study/reference material

---

## 🎓 Learning Resources

### PlantUML Basics
- Official Guide: https://plantuml.com/guide
- Interactive Tutorial: https://plantuml.com/starting
- Reference: https://plantuml.com/sitemap

### UML Basics
- UML Diagrams: https://www.uml-diagrams.org/
- Class Diagrams: https://www.uml-diagrams.org/class-diagrams.html
- Sequence Diagrams: https://www.uml-diagrams.org/sequence-diagrams.html

---

## 📧 Need Help?

1. Check the main `README.md` in this folder for detailed explanations
2. Review inline comments in each diagram
3. Examine the source code alongside the diagrams
4. Check PlantUML documentation for syntax questions

---

## 🎉 Quick Summary

```
📁 UML-Diagrams/
  ├── 📄 README.md                          ← Comprehensive guide
  ├── 📄 QUICK-START.md                     ← This file
  ├── 🏗️ ClassDiagram.md                    ← Classes & structure
  ├── 👤 UseCaseDiagram.md                  ← User functionality
  ├── 🔄 SequenceDiagram-Login.md           ← Login flow
  ├── 🔄 SequenceDiagram-CustomerCRUD.md    ← CRUD operations
  ├── 📦 ComponentDiagram.md                ← Architecture
  ├── 🔀 ActivityDiagram-MaterialManagement.md ← Workflows
  ├── 🌐 DeploymentDiagram.md               ← Infrastructure
  └── 🔄 StateDiagram-UserSession.md        ← Session lifecycle
```

**Start here:** Open `UseCaseDiagram.md` in VS Code with PlantUML extension!

---

*Last Updated: November 2025*

