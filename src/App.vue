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
  methods: {
    home: function() {
        /*eslint no-console: "off"*/
        console.log("home");
      this.$router.push({name: "Search"});
    }
  }
};
</script>
<style lang="scss">
@import "./assets/style/variables.scss";
@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
