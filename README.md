# 🛠️ Ticket Tracking System

A robust and role-based **Ticket Tracking System** developed using **.NET Core MVC**, enabling teams to effectively report, track, and resolve issues throughout the development lifecycle. This project is designed in two phases to incrementally add features and user types.

---

## 📌 Project Overview

This application manages tickets such as **Bugs**, **Feature Requests**, and **Test Cases**. It implements strict role-based permissions across different user types including:

- **QA (Quality Assurance)**  
- **RD (Research & Development)**  
- **PM (Project Manager)**  
- **Administrator**

---

## 🚀 Features

### 🔹 Phase I

- Two user roles: **QA** and **RD**
- **QA** users can:
  - Create bug tickets (Summary & Description required)
  - Edit or delete bug tickets
- **RD** users can:
  - View bugs
  - Mark bugs as **Resolved**

### 🔹 Phase II

- Adds new ticket fields:
  - **Severity** (e.g., Low, Medium, High)
  - **Priority** (e.g., P1, P2, P3)
- New user roles:
  - **PM**: Can create **Feature Requests**, RD resolves them
  - **Administrator**: Full system access, can manage all users
- New ticket types:
  - **Test Case**: Created and resolved by QA only (read-only for others)
  - **Feature Request**: Created by PM, resolved by RD

---

## 🧑‍💻 Use Cases

### 📋 Phase I Use Cases

- **QA**:
  - Log in
  - Create bug (with required Summary & Description)
  - Edit bug
  - Delete bug
  - View bugs

- **RD**:
  - Log in
  - View bugs
  - Resolve bugs

### 📋 Phase II Use Cases

- **QA**:
  - Create/Resolve **Test Cases**
  - View all tickets

- **RD**:
  - Resolve **Bugs** and **Feature Requests**
  - View all tickets

- **PM**:
  - Create **Feature Requests**
  - View all tickets

- **Administrator**:
  - Add/manage users (QA, RD, PM)
  - View/edit all tickets

---

## 🧰 Tech Stack

- **Backend**: ASP.NET Core MVC  
- **Frontend**: Razor Pages (or any preferred JS framework)  
- **Authentication**: ASP.NET Identity  
- **Database**: SQL Server / EF Core  

---

## 🧪 Getting Started

1. Clone the repo  
