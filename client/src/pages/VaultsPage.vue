<template>

    <section class="container-fluid bg-info">

        <div v-for="vault in vaults" :key="vault.id" class="col-9 col-md-3 m-2 card mb-2 mt-2">

            <VaultComponent :vault="vault" />

        </div>

    </section>

</template>


<script>
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js'
import VaultComponent from '../components/VaultComponent.vue';


export default {
    setup() {






        // async function getVaultsByAccountId(accountId) {
        //     try {
        //         await vaultsService.getVaultsByAccountId(accountId)
        //     }
        //     catch (error) {
        //         Pop.error(error);
        //     }
        // }





        // async function getVaultKeepsByAccountId(accountId){
        //     try {
        //       await vaultKeepsService.getVaultKeepsByAccountId(accountId)
        //     }
        //     catch (error){
        //       Pop.error(error);
        //     }
        // }

        // async function getVaultsByVaultKeepId(vaultKeepId){
        //     try {
        //       await vaultsService.getVaultsByVaultKeepId(vaultKeepId)
        //     }
        //     catch (error){
        //       Pop.error(error);
        //     }
        // }



        // watch(() => route.params.accountId, () => {
        //     getVaultsByAccountId(route.params.accountId)
        // })

        return {
            vaults: computed(() => AppState.accountVaults),
            // account: computed(() => AppState.account)
        }
    },
    components: { VaultComponent }
}
</script>


<style lang="scss" scoped>
.card {
    border: 2px solid black;
    border-radius: 16px;
    box-shadow: 3px 3px 10px rgba(42, 6, 134, 0.31);
}
</style>