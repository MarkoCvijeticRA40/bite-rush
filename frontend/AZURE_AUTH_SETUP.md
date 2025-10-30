# Azure AD Authentication Setup Instructions

Uspešno sam integrisao Azure AD autentifikaciju u vaš Bite Rush projekat. Evo šta sam dodao i kako da završite setup:

## Dodani fajlovi i komponente:

### 1. package.json
- Dodao sam Azure MSAL biblioteke: `@azure/msal-browser` i `@azure/msal-react`
- Dodao sam i ostale potrebne dependecies za React TypeScript projekat

### 2. Autentifikacija struktura:
```
src/
├── config/
│   └── authConfig.ts          # Azure AD konfiguracija
├── components/
│   ├── AuthButtons/
│   │   └── AuthButtons.tsx    # Sign In/Sign Out dugmad
│   ├── NavigationBar/
│   │   └── NavigationBar.tsx  # Navigacija sa auth statusom
│   └── PageLayout/
│       └── PageLayout.tsx     # Layout wrapper sa auth proverom
└── styles/
    └── auth.css              # Stilovi za autentifikaciju
```

### 3. Modifikovani fajlovi:
- `src/index.tsx` - Dodao MsalProvider wrapper
- `src/App.tsx` - Dodao PageLayout wrapper

## Sledeći koraci za završetak setup-a:

### 1. Instaliraj dependencies
```bash
npm install
```

### 2. Azure Portal Setup (OBAVEZNO pre testiranja)

**Korak 1: Kreiranje App Registration**
1. Idite na [Azure Portal](https://portal.azure.com)
2. Azure Active Directory → App registrations → New registration
3. Unesite:
   - Name: "Bite Rush Frontend"
   - Supported account types: "Accounts in any organizational directory and personal Microsoft accounts"
   - Redirect URI: Single-page application (SPA) → `http://localhost:3000`

**Korak 2: Kopiranje Client ID**
1. Nakon kreiranja, kopirajte "Application (client) ID"
2. U fajlu `src/config/authConfig.ts`, zamenite:
```typescript
clientId: "PASTE_YOUR_CLIENT_ID_HERE"
```

**Korak 3: Konfiguracija Authentication**
1. U vašoj aplikaciji idite na "Authentication"
2. Dodajte Redirect URIs:
   - `http://localhost:3000`
   - `http://localhost:3000/`
3. Logout URL: `http://localhost:3000`
4. Implicit grant and hybrid flows: ostavite prazno (SPA koristi PKCE)

### 4. Pokrenite aplikaciju
```bash
npm start
```

## Kako funkcioniše:

1. **NavigationBar** - Prikazuje "Sign in" dugme ako korisnik nije autentifikovan, ili korisničko ime i "Sign out" dugme ako jeste
2. **PageLayout** - Provera da li je korisnik autentifikovan pre prikazivanja sadržaja
3. **AuthButtons** - Rukuje sign in/sign out funkcionalnostima

## Dodatne mogućnosti:

- Možete dodati dodatne scope-ove u `loginRequest` u authConfig.ts
- Možete pristupiti korisničkim podacima koristeći `useMsal()` hook u bilo kojoj komponenti
- Za API pozive, možete koristiti access token-e

Autentifikacija je sada potpuno integrisana u vaš postojeći projekat!