import { Balance, EditNote, ExpandMore, Google } from "@mui/icons-material";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Container,
  Divider,
  List,
  ListItem,
  Stack,
  Typography,
} from "@mui/material";
import { MotionContainer } from "@vinicios-campera/kernel-react";

export default function Content() {
  return (
    <Container
      component={MotionContainer}
      sx={{ position: "relative", height: "100%" }}
    >
      <Stack spacing={1} sx={{ mt: 3 }}>
        <Stack padding={1} spacing={1} textAlign="start">
          <Typography variant="h5">
            Este Aplicativo recolhe alguns Dados Pessoais dos Usuários.
          </Typography>
          <Typography variant="body2">
            Este documento pode ser impresso para fins de consulta, através do
            comando imprimir nas configurações de seu navegador.
          </Typography>
        </Stack>

        <Divider />

        <Stack padding={1} spacing={1} textAlign="start">
          <Typography variant="h5">Tipos de Dados coletados</Typography>
          <Typography variant="caption">
            Entre os tipos de Dados Pessoais que este Aplicativo coleta, por si
            mesmo ou através de terceiros, existem: vários tipos de Dados.
          </Typography>
          <Typography variant="caption">
            Detalhes completos sobre cada tipo de Dados Pessoais coletados são
            fornecidos nas seções dedicadas desta política de privacidade ou por
            textos explicativos específicos exibidos antes da coleta de Dados.
            Os Dados Pessoais poderão ser fornecidos livremente pelo Usuário,
            ou, no caso dos Dados de Utilização, coletados automaticamente ao se
            utilizar este Aplicativo.
          </Typography>
          <Typography variant="caption">
            A menos que especificado diferentemente todos os Dados solicitados
            por este Aplicativo são obrigatórios e a falta de fornecimento
            destes Dados poderá impossibilitar este Aplicativo de fornecer os
            seus Serviços. Nos casos em que este Aplicativo afirmar
            especificamente que alguns Dados não forem obrigatórios, os Usuários
            ficam livres para deixarem de comunicar estes Dados sem nenhuma
            consequência para a disponibilidade ou o funcionamento do Serviço.
            Os Usuários que tiverem dúvidas a respeito de quais Dados Pessoais
            são obrigatórios estão convidados a entrar em contato com o
            Proprietário.
          </Typography>
          <Typography variant="caption">
            Quaisquer usos de cookies – ou de outras ferramentas de rastreamento
            por este Aplicativo ou pelos proprietários de serviços terceiros
            utilizados por este Aplicativo serão para a finalidade de fornecer
            os Serviços solicitados pelo Usuário, além das demais finalidades
            descritas no presente documento e na Política de Cookies, se estiver
            disponível.
          </Typography>
          <Typography variant="caption">
            Os Usuários ficam responsáveis por quaisquer Dados Pessoais de
            terceiros que forem obtidos, publicados ou compartilhados através
            deste Serviço (este Aplicativo) e confirmam que possuem a
            autorização dos terceiros para fornecerem os Dados para o
            Proprietário.
          </Typography>
        </Stack>

        <Divider />

        <Stack padding={1} spacing={1} textAlign="start">
          <Typography variant="h5">
            Modo e local de processamento dos Dados
          </Typography>
          <Typography variant="h6">Método de processamento</Typography>
          <Typography variant="caption">
            O Proprietário tomará as medidas de segurança adequadas para impedir
            o acesso não autorizado, divulgação, alteração ou destruição não
            autorizada dos Dados.
          </Typography>
          <Typography variant="caption">
            O processamento dos Dados é realizado utilizando computadores e /ou
            ferramentas de TI habilitadas, seguindo procedimentos
            organizacionais e meios estritamente relacionados com os fins
            indicados. Além do Proprietário, em alguns casos, os Dados podem ser
            acessados por certos tipos de pessoas encarregadas, envolvidas com a
            operação deste Serviço (este Aplicativo) (administração, vendas,
            marketing, administração legal do sistema) ou pessoas externas (como
            fornecedores terceirizados de serviços técnicos, carteiros,
            provedores de hospedagem, empresas de TI, agências de comunicação)
            nomeadas, quando necessário, como Processadores de Dados por parte
            do Proprietário. A lista atualizada destas partes pode ser
            solicitada ao Proprietário a qualquer momento.
          </Typography>
          <Typography variant="h6">
            Base jurídica para o processamento
          </Typography>
          <Typography variant="caption">
            O Proprietário poderá processar os Dados Pessoais relacionados ao
            Usuário se uma das hipóteses a seguir se aplicar:
          </Typography>
          <List dense={true}>
            <ListItem>
              <Typography variant="caption">
                os Usuários tenham dado a sua anuência para uma ou mais
                finalidades específicas; Observação: De acordo com algumas
                legislações o Proprietário poderá ter a permissão para processar
                os Dados Pessoais ATÉ QUE O Usuário faça objeção a isto
                (“opt-out”), sem ter que se basear em anuência ou em quaisquer
                outras bases jurídicas a seguir. Isto contudo não se aplica
                sempre que o processamento de Dados Pessoais estiver sujeito à
                legislação europeia de proteção de dados;
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                os Usuários tenham dado a sua anuência para uma ou mais
                finalidades específicas; Observação: De acordo com algumas
                legislações o Proprietário poderá ter a permissão para processar
                os Dados Pessoais ATÉ QUE O Usuário faça objeção a isto
                (“opt-out”), sem ter que se basear em anuência ou em quaisquer
                outras bases jurídicas a seguir. Isto contudo não se aplica
                sempre que o processamento de Dados Pessoais estiver sujeito à
                legislação europeia de proteção de dados;
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                o fornecimento dos Dados for necessário para o cumprimento de um
                contrato com o Usuário e/ou quaisquer obrigações pré-contratuais
                do mesmo;
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                o processamento for necessário para o cumprimento de uma
                obrigação jurídica à qual o Proprietário estiver sujeito;
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                o processamento estiver relacionado a uma tarefa que for
                executada no interesse público ou no exercício de uma
                autorização oficial na qual o Proprietário estiver investido;
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                o processamento for necessário para a finalidade de interesses
                legítimos perseguidos pelo Proprietário ou por um terceiro;
              </Typography>
            </ListItem>
          </List>
          <Typography variant="caption">
            Em qualquer caso, o Proprietário colaborará de bom grado para
            esclarecer qual a base jurídica que se aplica ao processamento, e em
            especial se o fornecimento de Dados for um requisito obrigatório por
            força de lei ou contratual, ou uma exigência necessária para
            celebrar um contrato.
          </Typography>
          <Typography variant="h6">Lugar</Typography>
          <Typography variant="caption">
            Os dados são processados ​​nas sedes de operação dos Proprietários,
            e em quaisquer outros lugares onde as partes envolvidas com o
            processamento estiverem localizadas. Dependendo da localização do
            Usuário as transferências de dados poderão envolver a transferência
            dos Dados do Usuário para outro país que não seja o seu. Para
            descobrirem mais sobre o local de processamento de tais Dados
            transferidos os Usuários poderão verificar a seção contendo os
            detalhes sobre o processamento de Dados Pessoais.
          </Typography>
          <Typography variant="caption">
            Os Usuários também possuem o direito de serem informados sobre a
            base jurídica das transferências de Dados para países de fora da
            União Europeia ou para quaisquer organizações internacionais regidas
            pelo direito internacional público ou formadas por dois ou mais
            países, tal como a ONU, e sobre as medidas de segurança tomadas pelo
            Proprietário para proteger os seus Dados.
          </Typography>
          <Typography variant="caption">
            Se ocorrerem quaisquer tais transferências os Usuários poderão
            descobrir mais a respeito verificando as seções pertinentes deste
            documento ou perguntando ao Proprietário utilizando as informações
            fornecidas na seção de contatos.
          </Typography>
          <Typography variant="h6">Período de conservação</Typography>
          <Typography variant="caption">
            Os Dados Pessoais serão processados e armazenados pelo tempo que for
            necessário para as finalidades para as quais forem coletados.
          </Typography>
          <Typography variant="caption">Portanto: </Typography>
          <List dense={true}>
            <ListItem>
              <Typography variant="caption">
                Os Dados Pessoais coletados para as finalidades relacionadas com
                a execução de um contrato entre o Proprietário e o Usuário serão
                conservados até que tal contrato tenha sido completamente
                cumprido.
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                Os Dados Pessoais coletados para as finalidades relacionadas com
                os legítimos interesses do Proprietário serão conservados pelo
                tempo que for necessário para cumprir tais finalidades. Os
                Usuários poderão obter informações específicas sobre os
                interesses legítimos perseguidos pelo Proprietário dentro das
                seções pertinentes deste documento ou entrando em contato com o
                Proprietário.
              </Typography>
            </ListItem>
            <ListItem>
              <Typography variant="caption">
                {" "}
                O Proprietário poderá ter a permissão de conservar os Dados
                Pessoais por um prazo maior sempre que o Usuário tiver dado a
                sua autorização para tal processamento, enquanto tal autorização
                não tiver sido retirada. Além disso, o Proprietário poderá ficar
                obrigado a conservar os Dados Pessoais por um prazo maior em
                todas as ocasiões em que estiver obrigado a fazê-lo para o
                cumprimento de uma obrigação jurídica ou em cumprimento de um
                mandado de uma autoridade.
              </Typography>
            </ListItem>
          </List>
          <Typography variant="caption">
            Assim que o prazo de conservação vencer os Dados Pessoais serão
            apagados. Desta forma o direito de acessar, o direito de apagar, o
            direito de corrigir e o direito à portabilidade dos dados não
            poderão ter o seu cumprimento exigido após o vencimento do prazo de
            conservação.
          </Typography>

          <Typography variant="h6">As finalidades do processamento</Typography>
          <Typography variant="caption">
            Os Dados relativos ao Usuário são coletados para permitir que o
            Proprietário preste seu Serviço, cumpra suas obrigações legais,
            responda a solicitações de execução, proteja seus direitos e
            interesses (ou aqueles de seus Usuários ou terceiros), detecte
            qualquer atividade maliciosa ou fraudulenta, assim como: Registro e
            autenticação.
          </Typography>
          <Typography variant="caption">
            Para obter informações específicas sobre os Dados Pessoais
            utilizados para cada finalidade, o Usuário poderá consultar a seção
            "Informações detalhadas sobre o processamento de Dados Pessoais".
          </Typography>
        </Stack>

        <Divider />

        <Stack padding={1} spacing={1} textAlign="start">
          <Typography variant="h5">
            Informações detalhadas sobre o processamento de Dados Pessoais
          </Typography>
          <Typography>
            Os Dados Pessoais são recolhidos para os seguintes fins e utilizando
            os seguintes serviços:
          </Typography>
          <Accordion>
            <AccordionSummary expandIcon={<ExpandMore />}>
              <Typography sx={{ width: "3%", flexShrink: 0 }}>
                <Google />
              </Typography>
              <Typography variant="h6">Registro e autenticação</Typography>
            </AccordionSummary>
            <AccordionDetails>
              <Typography variant="caption">
                Ao se registrar ou autenticar, os Usuários permitem a este
                serviço (este Aplicativo) identificá-los e dar-lhes o acesso a
                serviços dedicados. Dependendo do que estiver descrito abaixo,
                os serviços de registro e autenticação podem ser fornecidos por
                terceiros. Neste caso, este Aplicativo poderá acessar alguns
                Dados armazenados por estes serviços de terceiros para fins de
                registro ou identificação. Alguns dos serviços listados abaixo
                também podem coletar Dados Pessoais para fins de direcionamento
                e perfil; para saber mais, consulte a descrição de cada serviço.
              </Typography>
              <Typography variant="h6">Google OAuth (Google LLC)</Typography>
              <Typography variant="caption">
                O Google OAuth é um serviço de registro e autenticação fornecido
                pelo Google LLC e está conectado à rede Google.
              </Typography>
              <Typography variant="caption">
                Dados Pessoais processados: vários tipos de Dados como
                especificados na política de privacidade do serviço.
              </Typography>
              <Typography variant="caption">
                Lugar de processamento: EUA –{" "}
                <a href="https://policies.google.com/privacy">
                  Política de Privacidade.
                </a>
              </Typography>
            </AccordionDetails>
          </Accordion>
          <Accordion>
            <AccordionSummary expandIcon={<ExpandMore />}>
              <Typography sx={{ width: "3%", flexShrink: 0 }}>
                <EditNote />
              </Typography>
              <Typography variant="h6">
                Proteção igual dos Dados do Usuário
              </Typography>
            </AccordionSummary>
            <AccordionDetails>
              <Typography variant="caption">
                Este Aplicativo compartilha Dados do Usuário apenas com
                terceiros cuidadosamente selecionados, a fim de garantir que
                eles forneçam a mesma proteção de Dados de Usuário declarada
                nesta política de privacidade e exigida pelas leis de proteção
                de dados. Mais informações sobre as práticas de processamento de
                dados e privacidade por parte de terceiros podem ser encontradas
                em suas respectivas políticas de privacidade.
              </Typography>
            </AccordionDetails>
          </Accordion>
        </Stack>

        <Divider />

        <Stack padding={1} spacing={1} textAlign="start">
          <Typography variant="h5">Os direitos dos Usuários</Typography>
          <Typography>
            <Typography variant="caption">
              Os Usuários poderão exercer determinados direitos a respeito dos
              seus Dados processados pelo Proprietário.
            </Typography>
            <Typography variant="caption">
              Em especial, os Usuários possuem os direitos a fazer o seguinte:
            </Typography>
            <List dense={true}>
              <ListItem>
                <Typography variant="caption">
                  <strong>Retirar a sua anuência a qualquer momento.</strong> Os
                  Usuários possuem o direito de retirar a sua anuência nos casos
                  em que tenham dado a sua anuência anteriormente para o
                  processamento dos seus Dados Pessoais.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>Objetar o processamento dos seus Dados.</strong> Os
                  Usuários possuem o direito de objetar o processamento dos seus
                  Dados se o processamento for executado sobre outra base
                  jurídica que não a anuência. São fornecidos detalhes
                  adicionais na seção específica abaixo.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>Acessar os seus Dados.</strong> Os Usuários possuem o
                  direito de saber se os seus Dados estão sendo tratados pelo
                  Proprietário, obter revelações sobre determinados aspectos do
                  tratamento e conseguir uma cópia dos Dados que estiverem sendo
                  tratados.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>Verificar e pedir retificação.</strong> Os Usuários
                  possuem o direito de verificar a exatidão dos seus Dados e de
                  pedir que os mesmos sejam atualizados ou corrigidos.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>Restringir o processamento dos seus Dados.</strong> Os
                  Usuários possuem o direito de, sob determinadas
                  circunstâncias, restringir o processamento dos seus Dados para
                  qualquer outra finalidade que não seja o armazenamento dos
                  mesmos.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>
                    Ter os seus Dados Pessoais apagados ou retirados de outra
                    maneira.
                  </strong>{" "}
                  Os Usuários possuem o direito de, sob determinadas
                  circunstâncias, obter a eliminação dos seus Dados do
                  Proprietário.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>
                    Receber os seus Dados e ter os mesmos transferidos para
                    outro controlador.
                  </strong>{" "}
                  Os Usuários possuem o direito de receber os seus Dados em um
                  formato estruturado, utilizado comumente e apto a ser lido por
                  máquinas e, se for viável tecnicamente, fazer com que os
                  mesmos sejam transmitidos para outro controlador sem nenhum
                  empecilho. Esta determinação se aplica condicionada a que os
                  Dados sejam processados por meios automatizados e que o
                  processamento esteja baseado na anuência do Usuário, em um
                  contrato do qual o Usuário seja uma das partes ou por
                  obrigações pré-contratuais do mesmo.
                </Typography>
              </ListItem>
              <ListItem>
                <Typography variant="caption">
                  <strong>Registrar uma reclamação.</strong> Os Usuários possuem
                  o direito de apresentar reclamação perante a sua autoridade de
                  proteção de dados competente.
                </Typography>
              </ListItem>
            </List>
          </Typography>
          <Typography variant="h6">
            Detalhes sobre o direito de objetar ao processamento
          </Typography>
          <Typography variant="caption">
            Nos casos em que os Dados Pessoais forem processados por um
            interesse público, no exercício de uma autorização oficial na qual o
            Proprietário estiver investido ou para finalidades dos interesses
            legítimos perseguidos pelo Proprietário, os Usuários poderão objetar
            tal processamento através do fornecimento de um motivo relacionado
            com a sua situação em especial para justificar a objeção.
          </Typography>
          <Typography variant="caption">
            Os Usuários devem saber, entretanto, que caso os seus Dados Pessoais
            sejam processados para finalidades de marketing direto eles podem
            objetar tal processamento a qualquer momento sem fornecer nenhuma
            justificativa. Os Usuários podem consultar as seções respectivas
            deste documento.
          </Typography>
          <Typography variant="h6">Como exercer estes direitos</Typography>
          <Typography variant="caption">
            Quaisquer pedidos para exercer os direitos dos Usuários podem ser
            direcionados ao Proprietário através dos dados para contato
            fornecidos neste documento. Estes pedidos podem ser exercidos sem
            nenhum custo e serão atendidos pelo Proprietário com a maior
            brevidade possível e em todos os casos em prazo inferior a um mês.
          </Typography>
        </Stack>

        <Divider />

        <Stack padding={1} spacing={1} textAlign="start">
          <Typography variant="h5">
            Informações adicionais sobre a coleta e processamento de Dados
          </Typography>
          <Typography variant="h6">Ação jurídica</Typography>
          <Typography>
            <Typography variant="caption">
              Os Dados Pessoais dos Usuários podem ser utilizados para fins
              jurídicos pelo Proprietário em juízo ou nas etapas conducentes à
              possível ação jurídica decorrente de uso indevido deste Serviço
              (este Aplicativo) ou dos Serviços relacionados. O Usuário declara
              estar ciente de que o Proprietário poderá ser obrigado a revelar
              os Dados Pessoais mediante solicitação das autoridades
              governamentais.
            </Typography>
          </Typography>
          <Typography variant="h6">
            Informações adicionais sobre os Dados Pessoais do Usuário
          </Typography>
          <Typography variant="caption">
            Além das informações contidas nesta política de privacidade, este
            Aplicativo poderá fornecer ao Usuário informações adicionais e
            contextuais sobre os serviços específicos ou a coleta e
            processamento de Dados Pessoais mediante solicitação.
          </Typography>

          <Typography variant="h6">Logs do sistema e manutenção</Typography>
          <Typography variant="caption">
            Para fins de operação e manutenção, este Aplicativo e quaisquer
            serviços de terceiros poderão coletar arquivos que gravam a
            interação com este Aplicativo (logs do sistema) ou usar outros Dados
            Pessoais (tais como endereço IP) para esta finalidade.
          </Typography>

          <Typography variant="h6">
            As informações não contidas nesta política
          </Typography>
          <Typography variant="caption">
            Mais detalhes sobre a coleta ou processamento de Dados Pessoais
            podem ser solicitados ao Proprietário, a qualquer momento. Favor ver
            as informações de contato no início deste documento.
          </Typography>

          <Typography variant="h6">
            Como são tratados os pedidos de “Não me Rastreie”
          </Typography>
          <Typography variant="caption">
            Este Aplicativo não suporta pedidos de “Não Me Rastreie”. Para
            determinar se qualquer um dos serviços de terceiros que utiliza
            honram solicitações de “Não Me Rastreie”, por favor leia as
            políticas de privacidade.
          </Typography>

          <Typography variant="h6">
            Mudanças nesta política de privacidade
          </Typography>
          <Typography variant="caption">
            O Proprietário se reserva o direito de fazer alterações nesta
            política de privacidade a qualquer momento, através de notificação a
            seus Usuários nesta página e possivelmente dentro deste Serviço
            (este Aplicativo) e/ou – na medida em que for técnica e
            juridicamente viável – enviando um aviso para os Usuários através de
            quaisquer informações de contato disponíveis para o Proprietário. É
            altamente recomendável checar esta página regularmente, consultando
            a data da última modificação informada na parte inferior.
          </Typography>
          <Typography variant="caption">
            Caso as mudanças afetem as atividades de processamento realizadas
            com base na anuência do Usuário, o Proprietário coletará nova
            anuência do Usuário, onde for exigida.
          </Typography>
        </Stack>

        <Divider />

        <Accordion>
          <AccordionSummary expandIcon={<ExpandMore />}>
            <Typography sx={{ width: "3%", flexShrink: 0 }}>
              <Balance />
            </Typography>
            <Typography variant="h6">
              Definições e referências jurídicas
            </Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography variant="h6">Dados Pessoais (ou Dados)</Typography>
            <Typography variant="caption">
              Quaisquer informações que diretamente, indiretamente ou em relação
              com outras informações – incluindo um número de identificação
              pessoal – permitam a identificação ou identificabilidade de uma
              pessoa física.
            </Typography>
            <Typography variant="h6">Dados de Uso</Typography>
            <Typography variant="caption">
              As informações coletadas automaticamente através deste este
              Aplicativo (ou serviços de terceiros contratados neste Serviço
              (este Aplicativo)), que podem incluir: os endereços IP ou nomes de
              domínio dos computadores utilizados pelos Usuários que utilizam
              este Aplicativo, os endereços URI (Identificador Uniforme de
              Recurso), a data e hora do pedido, o método utilizado para
              submeter o pedido ao servidor, o tamanho do arquivo recebido em
              resposta, o código numérico que indica o status do servidor de
              resposta (resultado positivo, erro , etc.), o país de origem, as
              características do navegador e do sistema operacional utilizado
              pelo Usuário, os vários detalhes de tempo por visita (por exemplo,
              o tempo gasto em cada página dentro do aplicativo) e os detalhes
              sobre o caminho seguido dentro da aplicação, com especial
              referência à sequência de páginas visitadas e outros parâmetros
              sobre o sistema operacional do dispositivo e/ou ambiente de TI do
              Usuário.
            </Typography>
            <Typography variant="h6">Usuário</Typography>
            <Typography variant="caption">
              A pessoa que usa este Aplicativo que, a menos que especificado
              diferentemente, coincida com o Titular dos Dados.
            </Typography>

            <Typography variant="h6">Titular dos Dados</Typography>
            <Typography variant="caption">
              A pessoa física a quem os Dados Pessoais se referem.{" "}
            </Typography>

            <Typography variant="h6">
              Processador de Dados (ou supervisor de Dados)
            </Typography>
            <Typography variant="caption">
              A pessoa física ou jurídica, administração pública, agência ou
              outro órgão que processe os Dados Pessoais em nome do Controlador
              conforme descrito nesta política de privacidade.
            </Typography>

            <Typography variant="h6">
              Controlador de Dados (ou Proprietário)
            </Typography>
            <Typography variant="caption">
              A pessoa física ou jurídica, administração pública, agência ou
              outro órgão que, isoladamente ou em conjunto com outros determinar
              as finalidades e os meios de processamento dos Dados Pessoais,
              incluindo medidas de segurança relativas ao funcionamento e ao uso
              deste Serviço (este Aplicativo). O Controlador de Dados, a menos
              que seja especificado de outra forma, é o Proprietário deste
              Serviço (este Aplicativo).
            </Typography>

            <Typography variant="h6">Este Aplicativo</Typography>
            <Typography variant="caption">
              O meio pelo qual os Dados Pessoais do Usuário são coletados e
              processados.
            </Typography>

            <Typography variant="h6">Serviço</Typography>
            <Typography variant="caption">
              O serviço fornecido por este Aplicativo conforme descrito nos
              termos relativos (se disponíveis) e neste site/aplicativo.
            </Typography>

            <Typography variant="h6">União Europeia (ou UE)</Typography>
            <Typography variant="caption">
              A menos que especificado diferentemente, todas as referências
              feitas neste documento à União Europeia incluem todos os atuais
              estados membros da União Europeia e do Espaço Econômico Europeu.
            </Typography>

            <Divider />

            <Typography variant="h6">Informação jurídica</Typography>
            <Typography variant="caption">
              Esta declaração de privacidade foi preparada com base em
              determinações de múltiplas legislações, inclusive os Arts. 13/14
              do Regulamento (EU) 2016/679 (GDPR - Regulamento Geral de Proteção
              de Dados).
              <Typography variant="caption">
                Esta política de privacidade se refere somente a este
                Aplicativo, se não afirmado diferentemente neste documento.
              </Typography>
            </Typography>
          </AccordionDetails>
        </Accordion>
      </Stack>
    </Container>
  );
}
