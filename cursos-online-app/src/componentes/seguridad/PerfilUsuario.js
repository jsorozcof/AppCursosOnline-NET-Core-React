import React, { useEffect, useState } from "react";
import style from "../Tool/Style";
import { Container, Grid, TextField, Typography, Button } from "@material-ui/core";
import { actualizarUsuario, obtenerUsuarioActual } from "../../actions/UsuarioAction";
import { useStateValue } from "../../contexto/store";

const PerfilUsuario = () => {
  const [{ sesionUsuario }, dispatch] = useStateValue();
  const [usuario, setUsuario] = useState({
    nombreCompleto : '',
    email: '',
    password: '',
    confirmarPassword: '',
    username: ''
  })

  const  ingresarValoresMemoria = e => {
    const {name, value} = e.target;
    setUsuario(anterior => ({
      ...anterior,
      [name]: value
    }));
  }


  useEffect(() => {
    obtenerUsuarioActual(dispatch).then(response => {
        console.log('Esta es la data del usuairo actual', response);
        setUsuario(response.data);
    });
  }, [])
  
  const guardarUsuario = e => {
    e.preventDefault();
    actualizarUsuario(usuario).then(response => {
      if(response.status == 200){
        dispatch({
          type: "OPEN_SNACKBAR",
          openMensaje: {
            open: true,
            mensaje: "Se guardaron exitosamente los camnbio en el perfil usuario"
          }
        })

        window.localStorage.setItem("token_seguridad", response.data.token);

      }else {
        dispatch({
          type: "OPEN_SNACKBAR",
          openMensaje: {
            open: true,
            mensaje: "Errores al intentar guardar en : " +  Object.keys(response.data.errors)
          }
        })
      }
      //console.log('se actualizo el usuario', response);

    })
  }

  return (
    <Container componen="main" maxWidth="md" justity="center">
      <div style={style.paper}>
        <Typography component="h1" variant="h5">
          Perfil de Usuariox 
        </Typography>
      </div>

      <form style={style.form}>
        <Grid container spacing={2}>
          <Grid item xs={12} md={12}>
            <TextField
              name="nombreCompleto" value = {usuario.nombreCompleto} onChange = {ingresarValoresMemoria}
              variant="outlined"
              fullWidth
              label="Ingrese nombre y apellido"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="username" value = {usuario.username} onChange = {ingresarValoresMemoria}
              variant="outlined"
              fullWidth
              label="Ingrese Username"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="email" value = {usuario.email} onChange = {ingresarValoresMemoria}
              variant="outlined"
              fullWidth
              label="Ingrese email"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField 
              name="password"  value = {usuario.password} onChange = {ingresarValoresMemoria}
              type="password"
              variant="outlined"
              fullWidth
              label="Ingrese password"
            />
          </Grid>

          <Grid item xs={12} md={6}>
            <TextField
              name="confirmarPassword" value = {usuario.confirmarPassword} onChange = {ingresarValoresMemoria}
              type="password"
              variant="outlined"
              fullWidth
              label="Confirme password"
            />
          </Grid>
        </Grid>

        <Grid container justify="center">
          <Grid item xs={12} md={6}>
            <Button
              type="submit" onClick = {guardarUsuario}
              fullWidth
              variant="contained"
              size="large"
              color="primary"
              style={style.submit}
            >
              Guardar datos
            </Button>
          </Grid>
        </Grid>
      </form>
    </Container>
  );
};

export default PerfilUsuario;