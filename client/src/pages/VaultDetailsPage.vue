<template>
    <section v-if="vault" class="container-fluid">

        <div class="row">
            <div class="col-12">
                <h1>vault details page</h1>
                <h1>{{ vault.name }}</h1>
            </div>
        </div>

        <div class="row">
            <div v-for="keep in keeps" :key="keep.id" class="col-9 col-md-3 m-2 card mb-2 mt-2">
                <KeepComponent :keep="keep" />
            </div>
        </div>
    </section>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { vaultsService } from '../services/VaultsService';

export default {
    setup() {
        const route = useRoute();
        async function getKeepsByVaultId() {
            try {
                const vaultId = route.params.vaultId
                logger.log('fetching keeps', vaultId)
                await keepsService.getKeepsByVaultId(vaultId)
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getVaultById() {
            try {
                const vaultId = route.params.vaultId
                await vaultsService.getVaultById(vaultId)
            }
            catch (error) {
                useRouter().push('/')
            }
        }

        onMounted(() => {
            getKeepsByVaultId();
            getVaultById();
        })
        return {
            keeps: computed(() => AppState.keeps),
            vault: computed(() => AppState.activeVault)
        }
    }
}
</script>


<style lang="scss" scoped></style>