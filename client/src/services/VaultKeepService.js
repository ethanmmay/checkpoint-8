/* eslint-disable no-undef */
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { keepService } from '../services/KeepService'
import { api } from './AxiosService'
import { profileService } from './ProfileService'

class VaultKeepService {
  async createRelationship(vaultId, keepId) {
    try {
      const newRelationship = {
        vaultId: vaultId,
        keepId: keepId,
        creatorId: AppState.account.id
      }
      await api.post('api/vaultkeeps', newRelationship)
      await keepService.getKeepsByVaultId(vaultId)
      await profileService.getProfileKeeps(AppState.profile.id)
    } catch (err) {
      logger.error('Couldnt create VaultKeep', err)
    }
  }

  async deleteRelationship(keepId, vaultId) {
    try {
      const res = await api.get('api/vaultkeeps')
      AppState.vaultkeeps = res.data
      const vaultkeepId = AppState.vaultkeeps.filter(vk => vk.keepId === keepId && vk.vaultId === vaultId)[0].id
      if (window.confirm('Are you sure you want to remove this from your Vault?')) {
        await api.delete('api/vaultkeeps/' + vaultkeepId)
        keepService.getKeepsByVaultId(vaultId)
        document.getElementById('keepModalClose').click()
      }
    } catch (error) {
      logger.log(error)
    }
  }
}

export const vaultkeepService = new VaultKeepService()
