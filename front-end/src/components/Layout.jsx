function Layout({ children }) {
  return (
    <div className="layout-container">
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
      </header>

      <main className="layout-main">
        {children}
      </main>

      <footer style={{
        backgroundColor: "#0B3C5D",
        color: "white",
        textAlign: "center",
        padding: "1rem",
        fontSize: "0.9rem"
      }}>
        © {new Date().getFullYear()} SEALS Solutions. Todos os direitos reservados.
      </footer>
    </div>
  );
}


export default Layout;
