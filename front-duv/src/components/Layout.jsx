// src/components/Layout.jsx
import { Link } from "react-router-dom";

function Layout({ children }) {
  return (
    <div style={{ fontFamily: "Segoe UI, sans-serif", backgroundColor: "#f5f7fa", minHeight: "100vh" }}>
      <header style={{
        background: "#0B3C5D",
        padding: "1rem 2rem",
        color: "white",
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
        position: "sticky",
        top: 0,
        zIndex: 1000
      }}>
        <h2 style={{ margin: 0 }}>📄 Portal de DUVs</h2>
        <nav>
          <Link
            to="/"
            style={{
              color: "#fff",
              textDecoration: "none",
              fontWeight: "bold",
              fontSize: "1rem",
              borderBottom: "2px solid transparent",
              paddingBottom: "2px"
            }}
            onMouseOver={(e) => e.target.style.borderBottom = "2px solid white"}
            onMouseOut={(e) => e.target.style.borderBottom = "2px solid transparent"}
          >
            Início
          </Link>
        </nav>
      </header>

      <main style={{ padding: "2rem 3rem" }}>
        {children}
      </main>

      <footer style={{
        backgroundColor: "#0B3C5D",
        color: "white",
        textAlign: "center",
        padding: "1rem",
        marginTop: "4rem",
        fontSize: "0.9rem"
      }}>
        © {new Date().getFullYear()} SEALS Solutions. Todos os direitos reservados.
      </footer>
    </div>
  );
}

export default Layout;
