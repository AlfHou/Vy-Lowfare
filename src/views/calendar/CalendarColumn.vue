<template>
    <div class="calendar__column">
        <p><slot name="date"></slot></p>
        <p class="price">900</p>
    </div>
</template>
<script>
export default {
    
}
</script>
<script>
export default {
    
}
</script>
<style scoped lang="scss">
    @import "../../assets/style/variables.scss";
    .calendar__column{
        background-color: $light;
        height: 5rem;
        margin: 0;
    }
</style>