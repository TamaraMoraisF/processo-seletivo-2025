import { useNavioCrud } from "../hooks/useNavioCrud";
import Layout from "./Layout";
import { useNavigate } from "react-router-dom";

function NavioManager() {
  const navigate = useNavigate();
  const {
    navios,
    modalAberto,
    navioEmEdicao,
    setNavioEmEdicao,
    abrirFormularioNavio,
    salvarNavio,
    removerNavio,
    setModalAberto,
    erros
  } = useNavioCrud();


  const scrollCarousel = (direction) => {
    const carousel = document.getElementById("carousel");
    const scrollAmount = 400;
    if (carousel) {
      carousel.scrollBy({ left: scrollAmount * direction, behavior: "smooth" });
    }
  };

  return (
    <Layout>
      <div className="navios-page">
        <div className="navio-page-container">
          <div className="navios-header-clean">
            <div className="navios-header-titulo">
              <h2>Gerenciar Navios</h2>
            </div>
            <div className="navios-header-actions">
              <button className="botao botao-voltar" onClick={() => navigate("/")}>
                <span>🔙</span> Voltar
              </button>
              <button className="botao botao-adicionar-navio" onClick={abrirFormularioNavio}>
                <span>➕</span> Adicionar Navio
              </button>
            </div>
          </div>

          <div className="divisoria"></div>

          <div className="navios-carousel-container">
            <button className="carousel-button left" onClick={() => scrollCarousel(-1)}>‹</button>

            <div className="navios-carousel" id="carousel">
              {navios.map((navio) => (
                <div key={navio.navioGuid} className="navio-card">
                  <img src={navio.imagemUrl} alt={navio.nome} />
                  <div className="navio-card-body">
                    <h4>{navio.nome}</h4>
                    <div className="navio-detalhes">
                      <span>🚩 Bandeira: {navio.bandeira}</span>
                    </div>
                  </div>
                  <div className="navio-card-actions">
                    <button
                      className="botao botao-secundario"
                      onClick={() => abrirFormularioNavio(navio)}
                    >
                      Editar
                    </button>
                    <button
                      className="botao botao-perigo"
                      onClick={() => removerNavio(navio.navioGuid)}
                    >
                      Remover
                    </button>
                  </div>
                </div>
              ))}
            </div>

            <button className="carousel-button right" onClick={() => scrollCarousel(1)}>›</button>
          </div>

          {modalAberto && (
            <div className="modal">
              <h3>{navioEmEdicao.navioGuid ? "Editar" : "Adicionar"} Navio</h3>

              {/* Exibe erros */}
              {erros.length > 0 && (
                <div className="erros-validacao">
                  <ul>
                    {erros.map((erro, index) => (
                      <li key={index}>{erro}</li>
                    ))}
                  </ul>
                </div>
              )}

              <input
                placeholder="Nome"
                value={navioEmEdicao?.nome ?? ""}
                onChange={(e) => setNavioEmEdicao({ ...navioEmEdicao, nome: e.target.value })}
              />

              <input
                placeholder="Bandeira"
                value={navioEmEdicao?.bandeira ?? ""}
                onChange={(e) => setNavioEmEdicao({ ...navioEmEdicao, bandeira: e.target.value })}
              />

              <input
                placeholder="Imagem URL"
                value={navioEmEdicao?.imagemUrl ?? ""}
                onChange={(e) => setNavioEmEdicao({ ...navioEmEdicao, imagemUrl: e.target.value })}
              />

              <button className="botao botao-primario" onClick={salvarNavio}>Salvar</button>
              <button className="botao botao-perigo" onClick={() => setModalAberto(false)}>Cancelar</button>
            </div>
          )}

        </div>
      </div>
    </Layout>
  );
}

export default NavioManager;
