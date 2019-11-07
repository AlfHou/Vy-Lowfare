<template>
<body>
  <section class="hero is-primary is-bold">
    <div class="hero-body">
      <div class="container">
        <h1 class="title">Lowfare train</h1>
      </div>
    </div>
  </section>
  <section class="columns is-centered">
    <SearchView class="column is-three-quarters" />
  </section>
</body>
</template>

<script>
import SearchView from "./views/search/SearchView";

export default {
  name: "app",
  components: {
    SearchView
  }
};
</script>

<style lang="scss">
@import "~bulma/sass/utilities/_all";

$primary: #03265c;
$primary-invert: findColorInvert($primary);

$colors: (
  "white": (
    $white,
    $black
  ),
  "black": (
    $black,
    $white
  ),
  "light": (
    $light,
    $light-invert
  ),
  "dark": (
    $dark,
    $dark-invert
  ),
  "primary": (
    $primary,
    $primary-invert
  ),
  "info": (
    $info,
    $info-invert
  ),
  "success": (
    $success,
    $success-invert
  ),
  "warning": (
    $warning,
    $warning-invert
  ),
  "danger": (
    $danger,
    $danger-invert
  )
);

@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
