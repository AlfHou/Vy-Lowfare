<template>
<body>
  <section class="hero is-primary is-bold">
    <div class="hero-body">
      <div class="container">
        <a @click="home" class="title">Lowfare train</a>
      </div>
    </div>
  </section>
  <section class="columns is-centered">
    <router-view class="column is-three-quarters"></router-view>
  </section>
</body>
</template>

<script>
export default {
  name: "app",
  created: function() {
    this.$buefy.notification.open({
      duration: 30000,
      message: "Unfortunately, Vy has blocked the ip of the server " +
       "and the project therefore doesn't work properly. " +
        "The project should still be able to be run locally. " +
        "<br>See https://github.com/AlfHou/Vy-Lowfare for instructions on how to do so.",
        position: "is-top",
        type: "is-warning",
        hasIcon: true
    })
  },
  methods: {

    home: function() {
      this.$router.push({name: "Search"}).catch(() => {
        // Catch navigation duplicate and promptly ignore ;)
      })
    }
  }
};
</script>
<style lang="scss">
@import "./assets/style/variables.scss";
@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
