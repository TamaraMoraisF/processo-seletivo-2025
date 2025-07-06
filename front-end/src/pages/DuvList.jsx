import { Link } from "react-router-dom";
import { usePassageiroCrud } from "../hooks/usePassageiroCrud";
import { useNavioCrud } from "../hooks/useNavioCrud";
import { useDuvCrud } from "../hooks/useDuvCrud";
import "./../App.css";

function DuvList() {
  const {
    duvs,
    selectedDuv,
    setSelectedDuv,
    modalAberto: modalDuvAberto,
    duvEmEdicao,
    setDuvEmEdicao,
    abrirFormularioDuv,
    salvarDuv,
    removerDuv,
    setModalAberto: setModalDuvAberto
  } = useDuvCrud();

  const { navios } = useNavioCrud();

  const {
    modalAberto,
    passageiroEmEdicao,
    setPassageiroEmEdicao,
    abrirFormularioPassageiro,
    salvarPassageiro,
    removerPassageiro,
    setModalAberto
  } = usePassageiroCrud(selectedDuv, setSelectedDuv, () => { });

  return (
    <>
      {/* Botão navio + Sidebar */}
      <div>
        <div style={{ display: "flex", justifyContent: "center", paddingTop: "1rem" }}>
          <Link to="/navios" style={{ textDecoration: "none" }}>
            <button className="botao-navio">Gerenciar Navios</button>
          </Link>
        </div>

        <div className="sidebar">
          <button className="botao botao-primario botao-adicionar" onClick={() => abrirFormularioDuv()}>
            Adicionar DUV
          </button>

          <h2>DUVs</h2>
          {duvs.length === 0 && <p>Nenhuma DUV encontrada.</p>}
          {duvs.map((duv) => (
            <div
              key={duv.id}
              className={`duv-item ${selectedDuv?.id === duv.id ? "selected" : ""}`}
              onClick={() => setSelectedDuv(duv)}
            >
              <strong>DUV: {duv.numero}</strong>
              <br />
              <span>Data da viagem: {new Date(duv.dataViagem).toLocaleDateString()}</span>
            </div>
          ))}
        </div>
      </div>

      {/* Main */}
      <div className="main">
        {/* Modal de DUV */}
        {modalDuvAberto && (
          <div className="modal">
            <h3>{duvEmEdicao?.id ? "Editar DUV" : "Nova DUV"}</h3>

            <input
              placeholder="Número"
              value={duvEmEdicao.numero}
              onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, numero: e.target.value })}
            />

            <input
              type="date"
              value={
                duvEmEdicao.dataViagem
                  ? new Date(duvEmEdicao.dataViagem).toISOString().split("T")[0]
                  : ""
              }
              onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, dataViagem: e.target.value })}
            />

            <select
              value={duvEmEdicao.navioId}
              onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, navioId: parseInt(e.target.value) })}
            >
              <option value={0}>Selecione um navio</option>
              {navios.map((navio) => (
                <option key={navio.id} value={navio.id}>
                  {navio.nome} ({navio.bandeira})
                </option>
              ))}
            </select>

            <hr />
            <h4>Adicionar Passageiro/Tripulante</h4>

            <input
              placeholder="Nome"
              value={duvEmEdicao.nomePassageiro || ""}
              onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, nomePassageiro: e.target.value })}
            />
            <input
              placeholder="Nacionalidade"
              value={duvEmEdicao.nacionalidade || ""}
              onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, nacionalidade: e.target.value })}
            />
            <input
              placeholder="Foto URL"
              value={duvEmEdicao.fotoUrl || ""}
              onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, fotoUrl: e.target.value })}
            />
            <select
              value={duvEmEdicao.tipoPassageiro || 1}
              onChange={(e) =>
                setDuvEmEdicao({ ...duvEmEdicao, tipoPassageiro: parseInt(e.target.value) })
              }
            >
              <option value={1}>Passageiro</option>
              <option value={2}>Tripulante</option>
            </select>

            {duvEmEdicao.tipoPassageiro === 2 && (
              <input
                placeholder="SID"
                value={duvEmEdicao.sid || ""}
                onChange={(e) => setDuvEmEdicao({ ...duvEmEdicao, sid: e.target.value })}
              />
            )}

            <button
              className="botao botao-secundario"
              onClick={() => {
                const novaPessoa = {
                  nome: duvEmEdicao.nomePassageiro,
                  nacionalidade: duvEmEdicao.nacionalidade,
                  fotoUrl: duvEmEdicao.fotoUrl,
                  tipo: duvEmEdicao.tipoPassageiro,
                  sid: duvEmEdicao.tipoPassageiro === 2 ? duvEmEdicao.sid : null,
                };

                setDuvEmEdicao({
                  ...duvEmEdicao,
                  pessoas: [...(duvEmEdicao.pessoas || []), novaPessoa],
                  nomePassageiro: "",
                  nacionalidade: "",
                  fotoUrl: "",
                  tipoPassageiro: 1,
                  sid: "",
                });
              }}
            >
              Adicionar à lista
            </button>

            {/* Mostrar pessoas adicionadas */}
            {(duvEmEdicao.pessoas || []).map((p, i) => (
              <div key={i} className="pessoa-preview">
                <span><strong>{p.nome}</strong> – {p.tipo === 2 ? "Tripulante" : "Passageiro"}</span>
              </div>
            ))}

            <button className="botao botao-primario" onClick={salvarDuv}>Salvar</button>
            <button className="botao botao-perigo" onClick={() => setModalDuvAberto(false)}>Cancelar</button>
          </div>
        )}

        {/* Detalhes da DUV */}
        {selectedDuv ? (
          <>
            <h1>DUV: {selectedDuv.numero}</h1>

            <div className="duv-action-buttons">
              <button className="botao botao-secundario" onClick={() => abrirFormularioDuv(selectedDuv)}>Editar</button>
              <button className="botao botao-perigo" onClick={() => removerDuv(selectedDuv.id)}>Excluir</button>
            </div>

            <p><strong>Data da viagem:</strong> {new Date(selectedDuv.dataViagem).toLocaleDateString()}</p>

            <img
              src={selectedDuv.navio?.imagemUrl || "https://upload.wikimedia.org/wikipedia/commons/8/85/MSCVirtuosa.jpg"}
              alt="Navio"
              className="ship-img"
            />

            <p><strong>Nome do Navio:</strong> {selectedDuv.navio?.nome}</p>
            <p><strong>Bandeira:</strong> {selectedDuv.navio?.bandeira}</p>

            {/* Passageiros */}
            <h3>Passageiros</h3>
            <button className="botao botao-primario botao-adicionar" onClick={() => abrirFormularioPassageiro(null, 1)}>Adicionar Passageiro</button>
            <div className="pessoa-container">
              {selectedDuv.passageiros?.filter(p => !p.sid).map((p) => (
                <div key={p.id} className="pessoa-card">
                  <img src={p.fotoUrl} alt={p.nome} className="foto-pessoa" />
                  <span><strong>{p.nome}</strong></span>
                  <p>{p.nacionalidade}</p>
                  <button className="botao botao-secundario" onClick={() => abrirFormularioPassageiro(p)}>Editar</button>
                  <button className="botao botao-perigo" onClick={() => removerPassageiro(p.id)}>Remover</button>
                </div>
              ))}
            </div>

            {/* Tripulantes */}
            <div className="divisoria"></div>
            <h3>Tripulantes</h3>
            <button className="botao botao-primario botao-adicionar" onClick={() => abrirFormularioPassageiro(null, 2)}>Adicionar Tripulante</button>
            <div className="pessoa-container">
              {selectedDuv.passageiros?.filter(p => p.sid).map((p) => (
                <div key={p.id} className="pessoa-card">
                  <img src={p.fotoUrl} alt={p.nome} className="foto-pessoa" />
                  <span><strong>{p.nome}</strong></span>
                  <p>{p.nacionalidade}</p>
                  <p><em>SID:</em> {p.sid}</p>
                  <button className="botao botao-secundario" onClick={() => abrirFormularioPassageiro(p)}>Editar</button>
                  <button className="botao botao-perigo" onClick={() => removerPassageiro(p.id)}>Remover</button>
                </div>
              ))}
            </div>
          </>
        ) : (
          <div className="placeholder-container">
            <img
              src="https://cdn-icons-png.flaticon.com/512/744/744922.png"
              alt="Ícone DUV"
              className="placeholder-img"
            />
            <h2>Bem-vindo ao Portal de DUVs</h2>
            <p>Selecione uma DUV na lista ao lado para visualizar os detalhes da viagem.</p>
          </div>
        )}
      </div>

      {/* Modal passageiro/tripulante */}
      {modalAberto && (
        <div className="modal">
          <h3>{passageiroEmEdicao.id ? "Editar" : "Adicionar"} {passageiroEmEdicao.tipo === 2 ? "Tripulante" : "Passageiro"}</h3>

          <input
            placeholder="Nome"
            value={passageiroEmEdicao.nome}
            onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, nome: e.target.value })}
          />
          <input
            placeholder="Nacionalidade"
            value={passageiroEmEdicao.nacionalidade}
            onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, nacionalidade: e.target.value })}
          />
          <input
            placeholder="Foto URL"
            value={passageiroEmEdicao.fotoUrl}
            onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, fotoUrl: e.target.value })}
          />
          {passageiroEmEdicao.tipo === 2 && (
            <input
              placeholder="SID"
              value={passageiroEmEdicao.sid}
              onChange={(e) => setPassageiroEmEdicao({ ...passageiroEmEdicao, sid: e.target.value })}
            />
          )}

          <button className="botao botao-primario" onClick={salvarPassageiro}>Salvar</button>
          <button className="botao botao-perigo" onClick={() => setModalAberto(false)}>Cancelar</button>
        </div>
      )}
    </>
  );
}

export default DuvList;